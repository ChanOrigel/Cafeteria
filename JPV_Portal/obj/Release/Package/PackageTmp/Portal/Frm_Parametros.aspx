<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Parametros.aspx.cs" Inherits="JPV_Portal.Portal.Frm_Parametros" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Js_Parametros.js"></script>
</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container">
        <hr />

        <h2>Parámetros</h2>
        <hr />
    </section>
    <section class="container br-nav">
        <nav class="navbar">
            <div class="d-inline-flex " style="width: 100%;">
                <ul class="nav navbar-nav d-inline-flex">
                    <li class="myClass nav-item">
                        <ul class="list-inline-mb-0">
                            <li class="list-inline-item" id="li-nuevo">
                                <div>
                                    <button class="learn-more" id="btn_nuevo">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">NUEVO</span>
                                    </button>
                                </div>
                                <%--<button id="btn_nuevo" type="button" class="btn btn-primary btn_my_class" title="Nuevo"><i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nuevo</button>--%></li>
                            <li class="list-inline-item" id="li-editar">
                                <div>
                                    <button class="learn-more" id="btn_modificar">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">MODIFICAR</span>
                                    </button>
                                </div>
                                <%--<button id="btn_modificar" type="button" class="btn btn-primary btn_my_class" title="Modificar"><i class="glyphicon glyphicon-edit"></i>&nbsp;&nbsp;Modificar</button>--%></li>
                            <li class="list-inline-item" id="li-guardar" style="display: none">
                                <div>
                                    <button class="learn-more" id="btn_guardar">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">GUARDAR</span>
                                    </button>
                                </div>
                                <%--<button id="btn_guardar" type="button" class="btn btn-primary btn_my_class" title="Guardar"><i class="glyphicon glyphicon-floppy-save"></i>&nbsp;&nbsp;Guardar</button>--%></li>
                            <li class="list-inline-item" id="li-cancelar" style="display: none">
                                <div>
                                    <button class="learn-more" id="btn_cancelar">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">CANCELAR</span>
                                    </button>
                                </div>
                                <%--<button id="btn_cancelar" type="button" class="btn btn-primary btn_my_class" title="Cancelar"><i class="glyphicon glyphicon-remove"></i>&nbsp;&nbsp;Cancelar</button>--%>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </nav>
    </section>
    <div class="container">
        <input type="hidden" id="txt_id" />

        <h4>Datos Correo</h4>
        <hr />

        <label for="txt_no_proveedor"></label>
        <div class="row" id="cont-alta">

            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Correo</label>
                <div class="col-5">
                    <input type="text" id="txt_correo_saliente" class="form-control" placeholder="Correo" aria-describedby="basic-addon1" disabled="disabled"
                        onblur="this.value = (this.value.match(/^[^'#&\\]*$/))? this.value : this.value.replace(/'*#*&*\\*/gi,'');" />
                </div>

                <label for="example-text-input" class="col-1 col-form-label">*Servidor</label>
                <div class="col-5">
                    <input type="text" id="txt_servidor_correo" class="form-control input-sm" placeholder="Servidor" maxlength="100" aria-describedby="basic-addon1" disabled="disabled"
                        onblur="this.value = (this.value.match(/^[^'#&\\]*$/))? this.value : this.value.replace(/'*#*&*\\*/gi,'');" />
                    <br />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Contraseña</label>
                <div class="col-5">
                    <input type="password" id="txt_contrasenia_correo" class="form-control input-sm" placeholder="Contraseña" maxlength="50" disabled="disabled" />
                    <br />
                </div>

                <label for="example-text-input" class="col-1 col-form-label">*Puerto</label>
                <div class="col-5">
                    <input type="text" id="txt_puerto" class=" validacion form-control input-sm" tipo="numeros" placeholder="Puerto" maxlength="18" disabled="disabled" />
                </div>
                <br />
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Cifrar</label>
                <div class="col-5">
                    <select id="cmb_cifrar_correo" class="form-control" disabled="disabled">
                        <option value="Si">Si</option>
                        <option value="No" selected="selected">No</option>
                    </select>
                </div>
            </div>
            <div class="form-group row">
                <div class="col-sm-6 text-center">
                    <label for="txt_no_proveedor">Datos Correo Receptor</label>
                </div>
                <div class="col-sm-6 text-center">
                    <label for="txt_no_proveedor">Datos Técnicos</label>
                </div>
            </div>
            <hr />
            <div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Email Admin</label>
                <div class="col-5">
                    <input type="text" id="txt_correo_destino" class="form-control input-sm" maxlength="250" disabled="disabled" />
                </div>

                <label for="example-text-input" class="col-1 col-form-label">*Impresora</label>
                <div class="col-5">
                    <input type="text" id="txt_impresora" class="form-control" maxlength="100" disabled="disabled" />
                    <br />
                </div>
            </div>
            <%--<div class="form-group row">
                <label for="example-text-input" class="col-1 col-form-label">*Email Empleado</label>
                <div class="col-5">
                    <input type="text" id="txt_correo_empleado" class="form-control input-sm" maxlength="250" disabled="disabled" />
                </div>
            </div>--%>
        </div>
    </div>

</asp:Content>

