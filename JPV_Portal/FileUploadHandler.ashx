<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;
using System.IO;
using System.Drawing;

public class FileUploadHandler : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        string url = string.Empty;
        byte[] archivo_data = null;
        string nombre_archivo = string.Empty;
        string new_ruta = string.Empty;
        string _ruta_srv = String.Empty;
        string _ruta_srv_gral = String.Empty;
        
        if (context.Request.Files.Count > 0)
        {
            if (!String.IsNullOrEmpty(context.Request.QueryString["ruta"]))
                _ruta_srv = context.Request.QueryString["ruta"].ToString().Trim();
            if (!String.IsNullOrEmpty(context.Request.QueryString["ruta_gral"]))
                _ruta_srv_gral = context.Request.QueryString["ruta_gral"].ToString().Trim();
            
            HttpFileCollection files = context.Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];

                url = file.FileName;
                nombre_archivo = Path.GetFileName(url);

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (File.Exists(@url))
                        archivo_data = client.DownloadData(url);
                }

                if (!string.IsNullOrEmpty(_ruta_srv))
                {
                    if (!Directory.Exists(_ruta_srv_gral))
                        Directory.CreateDirectory(_ruta_srv_gral);
                    new_ruta = _ruta_srv;
                }
                else 
                {
                    if (!Directory.Exists(context.Server.MapPath("Temporal")))
                        Directory.CreateDirectory(context.Server.MapPath("Temporal"));
                    new_ruta = context.Server.MapPath("Temporal/" + nombre_archivo);
                }

                file.SaveAs(new_ruta);
            }
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}