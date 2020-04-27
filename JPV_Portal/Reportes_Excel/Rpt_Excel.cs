using System;
using OfficeOpenXml;
using System.IO;
using System.Data;
using OfficeOpenXml.Style;
using System.Globalization;
using JPV_Portal.Modelo.Negocio;
using System.Collections.Generic;

namespace JPV_Portal.Reportes_Excel
{
    public class Rpt_Excel 
    {
        FileInfo template;
        String ruta_nueva_archivo;
        int renglon = 2;
        int aux_renglon;
        //System.Data.DataTable Insumos;

        public Rpt_Excel(String ruta_plantilla, String ruta_nueva_archivo)
        {
            template = new FileInfo(ruta_plantilla);
            this.ruta_nueva_archivo = ruta_nueva_archivo;
            //this.Insumos = Lista_Insumos;
        }
        //public void Salida_Mercancia_Reporte(Cls_Mdl_Salida_Mercancia Objeto, List<Cls_Mdl_Salida_Mercancia> Lista)
        //{
        //    using (ExcelPackage p = new ExcelPackage(template, true))
        //    {

        //        ExcelWorksheet ws = p.Workbook.Worksheets[1];
        //        CultureInfo ci = new CultureInfo("en-us");

        //        aux_renglon = renglon;
        //        String s = String.Empty;

        //        for (int i = 0; i < Lista.Count; i++)
        //        {
        //            ws.Cells[renglon, 1].Value = Lista[i].Proveedor.ToString();
        //            ws.Cells[renglon, 2].Value = Lista[i].Producto.ToString();
        //            ws.Cells[renglon, 3].Value = Lista[i].Chofer.ToString();
        //            ws.Cells[renglon, 4].Value = Lista[i].Cantidad.ToString();
        //            ws.Cells[renglon, 5].Value = Objeto.Total.ToString();
                       
        //            renglon++;

        //        }

        //            ws.Cells[aux_renglon, 1, renglon - 1, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[aux_renglon, 1, renglon - 1, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[aux_renglon, 1, renglon - 1, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //            ws.Cells[aux_renglon, 1, renglon - 1, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        //            Byte[] bin = p.GetAsByteArray();
        //            String file = ruta_nueva_archivo;
        //            File.WriteAllBytes(file, bin);
        //    }


        //}
    }
}