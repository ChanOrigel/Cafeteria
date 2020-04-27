using System;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Style;
using System.Globalization;
using JPV_Portal.Modelo.Negocio;

namespace JPV_Portal.ReportesExcel
{
    public class Rpt_Inventarios
    {
        FileInfo template;
        String ruta_nueva_archivo;
        int renglon = 8;
        int Renglon = 7;
        int aux_renglon;
        System.Data.DataTable Insumos;


        public Rpt_Inventarios(String ruta_plantilla, String ruta_nueva_archivo, System.Data.DataTable Tabla)
        {
            template = new FileInfo(ruta_plantilla);
            this.ruta_nueva_archivo = ruta_nueva_archivo;
            this.Insumos = Tabla;
        }

        public void Crear_Corte(Cls_Mdl_Balance Datos)
        {

            using (ExcelPackage p = new ExcelPackage(template, true))
            {
                var date = DateTime.Now.ToString("dd/MM/yyyy");

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                aux_renglon = renglon;

                ws.Cells[1, 4].Value = Datos.Usuario_Creo;
                ws.Cells[2, 4].Value = Datos.Inicio_Caja;
                ws.Cells[3, 4].Value = Datos.Fin_Caja;

                ws.Cells[1, 8].Value = date;
                ws.Cells[2, 8].Value = Datos.Ventas;


                foreach (DataRow Dr in Insumos.Rows)
                {
                    ws.Cells[renglon, 1].Value = Dr["Folio"].ToString();
                    ws.Cells[renglon, 2].Value = Dr["Cantidad"].ToString();
                    ws.Cells[renglon, 3].Value = Dr["Categoria"].ToString();
                    ws.Cells[renglon, 4].Value = Dr["Producto"].ToString();
                    ws.Cells[renglon, 6].Value = Dr["Importe"].ToString();
                    renglon++;

                }

                ws.Cells[aux_renglon, 1, renglon - 1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, renglon - 1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, renglon - 1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, renglon - 1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // guarda los cambios
                Byte[] bin = p.GetAsByteArray();
                String file = ruta_nueva_archivo;
                File.WriteAllBytes(file, bin);
            }
        }
        public void Enviar_Reporte(Cls_Mdl_Balance Datos)
        {

            using (ExcelPackage p = new ExcelPackage(template, true))
            {
                var date = DateTime.Now.ToString("dd/MM/yyyy");

                ExcelWorksheet ws = p.Workbook.Worksheets[1];

                aux_renglon = Renglon;

                if (!string.IsNullOrEmpty(Datos.Fecha_Inicio))
                    ws.Cells[3, 5].Value = Datos.Fecha_Inicio;
                else
                    ws.Cells[3, 5].Value = date;

                if (!string.IsNullOrEmpty(Datos.Fecha_Fin))
                    ws.Cells[4, 5].Value = Datos.Fecha_Fin;
                else
                    ws.Cells[4, 5].Value = date;

                decimal venta = 0;
                foreach (DataRow Dr in Insumos.Rows)
                {
                    ws.Cells[Renglon, 1].Value = Dr["Folio"].ToString();
                    ws.Cells[Renglon, 2].Value = Dr["Categoria"].ToString();
                    ws.Cells[Renglon, 3].Value = Dr["Producto"].ToString();
                    ws.Cells[Renglon, 4].Value = Dr["Cantidad"].ToString();
                    ws.Cells[Renglon, 5].Value = Dr["Precio"].ToString();
                    ws.Cells[Renglon, 6].Value = Dr["Importe"].ToString();
                    Renglon++;

                    venta += System.Convert.ToDecimal(Dr["Importe"].ToString());
                }

                ws.Cells[3, 2].Value = venta;


                ws.Cells[aux_renglon, 1, Renglon - 1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[aux_renglon, 1, Renglon - 1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // guarda los cambios
                Byte[] bin = p.GetAsByteArray();
                String file = ruta_nueva_archivo;
                File.WriteAllBytes(file, bin);
            }
        }

    }
}