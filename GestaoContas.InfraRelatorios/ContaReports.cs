using GestaoContas.Dominio.Entities;
using GestaoContas.Dominio.Interfaces.Reports;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.InfraRelatorios
{
    public class ContaReports : IContaReports
    {
        public byte[] CreateExcel(List<Conta> conta)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var excelPackage = new ExcelPackage())
            {
                var planilha = excelPackage.Workbook.Worksheets.Add("Contas");
                planilha.Cells["A1"].Value = "Relatório de Contas";

                var titulo = planilha.Cells["A1:D1"];
                titulo.Merge = true;
                titulo.Style.Font.Size = 16;
                titulo.Style.Font.Bold = true;
                titulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                planilha.Cells["A2"].Value = $"Gerado em: {DateTime.Now.ToString("dd/MM/yyyy")}";
                var subtitulo = planilha.Cells["A2:D2"];
                subtitulo.Merge = true;
                subtitulo.Style.Font.Size = 14;
                subtitulo.Style.Font.Bold = true;
                subtitulo.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                planilha.Cells["A4"].Value = "Nome da Conta";
                planilha.Cells["B4"].Value = "Data";
                planilha.Cells["C4"].Value = "Valor";
                planilha.Cells["D4"].Value = "Tipo";

                var cabecalhos = planilha.Cells["A4:D4"];
                cabecalhos.Style.Font.Size = 12;
                cabecalhos.Style.Font.Bold = true;
                cabecalhos.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                cabecalhos.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cabecalhos.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#363636"));

                var linha = 5;
                foreach(var item in conta)
                {
                    planilha.Cells[$"A{linha}"].Value = item.Nome;
                    planilha.Cells[$"B{linha}"].Value = item.Data.ToString("dd/MM/yyyy");
                    planilha.Cells[$"C{linha}"].Value = item.Valor.ToString("c");
                    planilha.Cells[$"D{linha}"].Value = item.Tipo.ToString();
                    linha++;

                    if(linha % 2 == 0)
                    {
                        var conteudo = planilha.Cells[$"A{linha}:D{linha}"];
                        conteudo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        conteudo.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EEEEEE"));
                    }
                }

                var dados = planilha.Cells[$"A4:D{linha-1}"];
                dados.Style.Border.BorderAround(ExcelBorderStyle.Medium);

                planilha.Cells[$"A{linha}"].Value = $"Quantidade:";
                planilha.Cells[$"B{linha}"].Value = $"{linha - 5}";
                var rodape = planilha.Cells[$"A{linha}:D{linha}"];
                rodape.Style.Font.Size = 12;
                rodape.Style.Font.Bold = true;
                rodape.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                rodape.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rodape.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#363636"));
                dados.Style.Border.BorderAround(ExcelBorderStyle.Medium);

                planilha.Cells["A:D"].AutoFitColumns();
                return excelPackage.GetAsByteArray();
            }
        }

        public byte[] CreatePdf(List<Conta> conta)
        {
            var memoryStream = new MemoryStream();
            var pdf = new PdfDocument(new PdfWriter(memoryStream));
            using (var document = new Document(pdf))
            {
                //var logo = ImageDataFactory.Create("https://git-scm.com/images/logos/1color-darkbg@2x.png");
                //document.Add(new iText.Layout.Element.Image(logo));

                document.Add(new Paragraph("Relatório de Contas").AddStyle(new Style().SetFontSize(16)));
                document.Add(new Paragraph($"Gerado em: {DateTime.Now.ToString("dd/MM/yyyy")}").AddStyle(new Style().SetFontSize(14)));
                document.Add(new Paragraph("\n"));

                var table = new Table(4);
                table.SetWidth(UnitValue.CreatePercentValue(100));
                table.AddHeaderCell(new Paragraph("Nome da Conta").AddStyle(new Style().SetFontSize(14)));
                table.AddHeaderCell(new Paragraph("Data").AddStyle(new Style().SetFontSize(14)));
                table.AddHeaderCell(new Paragraph("Valor").AddStyle(new Style().SetFontSize(14)));
                table.AddHeaderCell(new Paragraph("Tipo").AddStyle(new Style().SetFontSize(14)));

                foreach (var item in conta)
                {
                    table.AddCell(new Paragraph(item.Nome).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.Data.ToString("dd/MM/yyyy")).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.Valor.ToString("c")).AddStyle(new Style().SetFontSize(12)));
                    table.AddCell(new Paragraph(item.Tipo.ToString()).AddStyle(new Style().SetFontSize(12)));
                }

                document.Add(table);
                document.Add(new Paragraph($"Quantidade: {conta.Count}").AddStyle(new Style().SetFontSize(12)));
            }

            return memoryStream.ToArray();
        }
    }
}
