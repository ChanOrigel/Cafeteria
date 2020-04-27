<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Balance.aspx.cs" Inherits="JPV_Portal.Portal.Frm_A_Pagar" %>


<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../Recursos/plugins/accounting.min.js"></script>
    <script src="../JS/Js_Balance.js"></script>

</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container">
        <hr />
        <h2>Cortes de caja y Reportes</h2>
        <hr />
    </section>
    <section class="container br-nav">
        <nav class="navbar">
            <div class="d-inline-flex " style="width: 100%;">
                <ul class="nav navbar-nav d-inline-flex">
                    <li class="myClass nav-item">
                        <ul class="list-inline-mb-0">
                            <li class="list-inline-item" id="li-cortes">
                                <div>
                                    <button class="learn-more" id="btn_cortes">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">Hacer Corte</span>
                                    </button>
                                </div>
                            </li>
                            <li class="list-inline-item" id="li-reportes">
                                <div>
                                    <button class="learn-more" id="btn_reportes">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">Reporte Gral.</span>
                                    </button>
                                </div>
                            </li>
                        </ul>
                    </li>
                </ul>
            </div>
        </nav>
    </section>
    <hr />
    <div class="container">
        <input type="hidden" id="Txt_Balance_Id" />
        <div class="form-group row">
            <div class="col-8"></div>
            <span class="fa fa-calendar">
                <label for="example-text-input" class="col-1 col-form-label">Fecha</label></span>
            <div class="col-2">
                <input type="text" id="Txt_Fecha" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
            </div>
        </div>
        <br />
        <div class="form-group row">
            <label for="example-text-input" class="col-2 col-form-label">Inicio en caja</label>
            <div class="col-2">
                <input type="text" id="Txt_Caja_Inicio" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
            </div>
            <button class="btn btn-warning" id="btn_actualizar">Actualizar</button>

            <div class="col-2"></div>
            <label for="example-text-input" class="col-2 col-form-label">Ventas del dia</label>
            <div class="col-2">
                <input type="text" id="Txt_Ventas" class="form-control" disabled="disabled" aria-describedby="basic-addon1" />
            </div>
        </div>
        <br />
        <div class="row" id="Reg-Datos">
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
        </div>

        <%-- MODAL PARA ENVIO DEL REPORTE POR RANGO DE FECHAS --%>
        <div id="Modal_Reporte" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbspENVIAR REPORTE</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-2">
                        </div>
                        <span class="fa fa-calendar">
                            <label for="example-text-input" class="col-1 col-form-label">Inicio</label></span>
                        <div class="col-3">
                            <input type="text" id="Txt_Fecha_Inicial" class="form-control" placeholder="Fecha" aria-describedby="basic-addon1" />
                        </div>
                        <span class="fa fa-calendar">
                            <label for="example-text-input" class="col-1 col-form-label">Final</label></span>
                        <div class="col-3">
                            <input type="text" id="Txt_Fecha_Final" class="form-control" placeholder="Fecha" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="Enviar_Reporte()">Enviar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


        <%-- MODAL PARA ENVIO DEL CORTE DE TURNO --%>
        <div id="Modal_Corte" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HACER CORTE DE TURNO</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-3">
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label" id="Etiquetas">Fin de Caja</label>
                        <div class="col-3">
                            <input type="text" id="Txt_Caja_Fin" class="form-control" placeholder="$" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="validar_datos('corte')">Generar Corte</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


        <%-- MODAL PARA ACTUALIZAR EL INICIO EN CAJA --%>
        <div id="Modal_Actualizar" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">

                        <h4 class="modal-title">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;ACTUALIZAR CAJA</h4>

                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <br />
                    <div class="form-group row">
                        <div class="col-md-3">
                        </div>
                        <label for="example-text-input" class="col-3 col-form-label">Inicio en Caja</label>
                        <div class="col-3">
                            <input type="text" id="Txt_Nuevo_Inicio" class="form-control" aria-describedby="basic-addon1" />
                        </div>
                    </div>
                    <div class="row" data-bind="visible: hasTI">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" onclick="validar_datos('actualizar')">Actualizar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
