<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="RioGrande-Usuario.aspx.cs" Inherits="JPV_Portal.Portal.RioGrande_Usuario" %>

<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Usuarios.js?v=1.0.0"></script>
</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container">
        <hr />
        <h2>Usuarios</h2>
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
                                <%--<button id="btn_nuevo" type="button" class="btn btn-primary btn_my_class" title="Nuevo"><i class="glyphicon glyphicon-plus"></i>&nbsp;&nbsp;Nuevo</button>--%>
                            </li>
                            <li class="list-inline-item" id="li-guardar" style="display: none">
                                <div>
                                    <button class="learn-more" id="btn_guardar">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">GUARDAR</span>
                                    </button>
                                </div>
                                <%--<button id="btn_guardar" type="button" class="btn btn-primary btn_my_class" title="Guardar"><i class="glyphicon glyphicon-floppy-save"></i>&nbsp;&nbsp;Guardar</button>--%>

                            </li>
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
    <hr />
    <div class="container">
        <input type="hidden" id="txt_usuario_id" />
        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>
        <div class="row" id="cont-alta" style="display: none;">
            <div class="form-check form-check-inline">
                <label class="form-check-label">
                    <input class="form-check-input" type="radio" name="Tipo" id="chk_Administrador" value="Administrador" />
                    Administrador
                </label>
            </div>
            <div class="form-check form-check-inline">
                <label class="form-check-label">
                    <input class="form-check-input" type="radio" name="Tipo" id="chk_Empleado" value="Empleado" />
                    Empleado
                </label>
            </div>
            <hr />
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Usuario</label>
                <div class="col-10">
                    <input type="text" id="txt_nombre" class="form-control" placeholder="Nombre" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Email</label>
                <div class="col-10">
                    <input type="text" id="txt_email" class="form-control" placeholder="Correo electrónico" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label">*Contraseña</label>
                <div class="col-4">
                    <input type="password" id="txt_password" class="form-control" placeholder="Contraseña" aria-describedby="basic-addon1" />
                </div>
                <label for="example-text-input" class="col-2 col-form-label">*Estatus</label>
                <div class="col-4">
                    <select id="cmb_estatus" class="form-control text-center">
                        <option value="" selected="selected">Seleccione</option>
                        <option value="ACTIVO">ACTIVO</option>
                        <option value="INACTIVO">INACTIVO</option>
                    </select>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
