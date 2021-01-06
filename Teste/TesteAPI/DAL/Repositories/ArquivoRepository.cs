using DAL.Entities;
using DAL.Repositories.Interfaces;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        private IDespesaRepository _despesaRepository;

        public ArquivoRepository(ApplicationDbContext context, IDespesaRepository despesaRepository) : base(context)
        {
            _despesaRepository = despesaRepository;
        }


        public override IEnumerable<Arquivo> GetAll()
        {
            return _entities
                .Include(p => p.Cliente)
                .Include(p => p.Despesas)
                .Select(p => new Arquivo()
                {
                    IdArquivo = p.IdArquivo,
                    IdCliente = p.IdCliente,
                    NomeArquivo = p.NomeArquivo,
                    DataEnvio = p.DataEnvio,
                    DespesasTotal = p.Despesas.Count,
                    Cliente = p.Cliente
                }).OrderByDescending(p => p.DataEnvio);
        }

        public string ProcessarArquivo(int idCliente, IFormFile file)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (file.Length > 0)
                    {
                        var arquivo =
                        _entities.Add(new Arquivo()
                        {
                            IdCliente = idCliente,
                            DataEnvio = DateTime.Now,
                            NomeArquivo = file.FileName
                        });

                        _context.SaveChanges();

                        switch (file.ContentType)
                        {
                            case "application/json":
                                ProcessarArquivoJSON(arquivo.Entity, file);
                                break;
                            case "application/vnd.ms-excel":
                                ProcessarArquivoCSV(arquivo.Entity, file);
                                break;
                            case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                                ProcessarArquivoExcel(arquivo.Entity, file);
                                break;
                            default:
                                return "O arquivo enviado não é suportado";
                        }

                        _context.SaveChanges();
                        dbContextTransaction.Commit();

                        return null;
                    }
                    else
                        return "Arquivo vazio.";
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    return $"Erro Interno: { ex }";
                }
            }
        }

        private string ProcessarArquivoCSV(Arquivo arquivo, IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                int tipoConta_Index = 0;
                int valorCobrado_Index = 0;
                int dataVencimento_Index = 0;
                int dataPagamento_Index = 0;
                int valorPago_Index = 0;
                int valorMulta_Index = 0;
                bool firstIteration = true;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.ToLower().Split(';');

                    if (firstIteration)
                    {
                        tipoConta_Index = Array.FindIndex(values, x => x.Contains("tipo conta"));
                        valorCobrado_Index = Array.FindIndex(values, x => x.Contains("valor cobrado"));
                        dataVencimento_Index = Array.FindIndex(values, x => x.Contains("data vencimento"));
                        dataPagamento_Index = Array.FindIndex(values, x => x.Contains("data pagamento"));
                        valorPago_Index = Array.FindIndex(values, x => x.Contains("valor pago"));
                        valorMulta_Index = Array.FindIndex(values, x => x.Contains("valor multa"));

                        firstIteration = false;
                    }
                    else
                    {
                        _despesaRepository.Add(new Despesa()
                        {
                            IdArquivo = arquivo.IdArquivo,
                            TipoDespesa = GetTipoDespesa(values[tipoConta_Index]),
                            ValorCobrado = GetDecimalValue(values[valorCobrado_Index]),
                            DataVencimento = GetDateTimeValue(values[dataVencimento_Index]),
                            DataPagamento = GetDateTimeValue(values[dataPagamento_Index]),
                            ValorPago = GetDecimalValue(values[valorPago_Index]),
                            ValorMulta = GetDecimalValue(values[valorMulta_Index]),
                        });
                    }
                }
            }

            return "";
        }

        private string ProcessarArquivoExcel(Arquivo arquivo, IFormFile file)
        {
            using (var reader = ExcelReaderFactory.CreateReader(file.OpenReadStream()))
            {
                var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });

                var tables = result.Tables
                   .Cast<DataTable>()
                   .Select(t => new
                   {
                       TableName = t.TableName,
                       Columns = t.Columns
                            .Cast<DataColumn>()
                            .Select(x => x.ColumnName.ToLower())
                            .ToList()
                   });

                int tipoConta_Index = tables.First().Columns.FindIndex(x => x.Contains("tipo conta"));
                int valorCobrado_Index = tables.First().Columns.FindIndex(x => x.Contains("valor cobrado"));
                int dataVencimento_Index = tables.First().Columns.FindIndex(x => x.Contains("data vencimento"));
                int valorPago_Index = tables.First().Columns.FindIndex(x => x.Contains("data pagamento"));
                int valorMulta_Index = tables.First().Columns.FindIndex(x => x.Contains("valor pago"));
                int dataPagamento_Index = tables.First().Columns.FindIndex(x => x.Contains("valor multa"));

                foreach (DataTable table in result.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        _despesaRepository.Add(new Despesa()
                        {
                            IdArquivo = arquivo.IdArquivo,
                            TipoDespesa = GetTipoDespesa(row.ItemArray[tipoConta_Index].ToString()),
                            ValorCobrado = GetDecimalValue(row.ItemArray[valorCobrado_Index].ToString()),
                            DataVencimento = GetDateTimeValue(row.ItemArray[dataVencimento_Index].ToString()),
                            DataPagamento = GetDateTimeValue(row.ItemArray[valorPago_Index].ToString()),
                            ValorPago = GetDecimalValue(row.ItemArray[valorMulta_Index].ToString()),
                            ValorMulta = GetDecimalValue(row.ItemArray[dataPagamento_Index].ToString())
                        });
                    }
                }
            }

            return "";
        }

        private string ProcessarArquivoJSON(Arquivo arquivo, IFormFile file)
        {
            using (Stream stream = file.OpenReadStream())
            using (StreamReader sr = new StreamReader(stream))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                string json = sr.ReadToEnd();
                List<Despesa> despesas = JsonConvert.DeserializeObject<List<Despesa>>(json);

                foreach (var despesa in despesas)
                {
                    despesa.IdArquivo = arquivo.IdArquivo;
                    _despesaRepository.Add(despesa);
                }
            }

            return "";
        }

        private TipoDespesa GetTipoDespesa(string tipo)
        {
            tipo = tipo.ToLower().Trim();

            if (tipo.Contains("agua") || tipo.Contains("água"))
                return TipoDespesa.Conta_Agua;
            else if (tipo.Contains("gas") || tipo.Contains("gás"))
                return TipoDespesa.Conta_Gas;
            else if (tipo.Contains("hospedagem"))
                return TipoDespesa.Conta_Hospedagem;
            else if (tipo.Contains("internet"))
                return TipoDespesa.Conta_Internet;
            else if (tipo.Contains("locacao") || tipo.Contains("locação"))
                return TipoDespesa.Conta_Locacao;
            else if (tipo.Contains("luz"))
                return TipoDespesa.Conta_Luz;
            else
                return TipoDespesa.Nao_Identificado;
        }

        private decimal? GetDecimalValue(string value)
        {
            value = value.Trim();
            decimal decimalParse;

            if (string.IsNullOrEmpty(value))
                return null;
            else
                Decimal.TryParse(value, out decimalParse);

            return decimalParse;
        }

        private DateTime? GetDateTimeValue(string date)
        {
            date = date.Trim();
            DateTime dateTimeParse;

            if (string.IsNullOrEmpty(date))
                return null;
            else
                DateTime.TryParse(date, out dateTimeParse);

            return dateTimeParse;
        }
    }
}
