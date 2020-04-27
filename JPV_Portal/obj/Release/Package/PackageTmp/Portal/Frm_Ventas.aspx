<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Ventas.aspx.cs" Inherits="JPV_Portal.Portal.Frm_Ventas" %>


<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <script src="../JS/Js_Ventas.js"></script>
    <link href="../Recursos/Css/StyleSheet.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="RioGrande" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <section class="container">
        <hr />
        <h2>VENTAS</h2>
        <hr />
    </section>
    <br />
    <div class="container">
        <input type="hidden" id="txt_producto_id" />
        <input type="hidden" id="txt_producto" />
        <input type="hidden" id="txt_categoria_id" />
        <input type="hidden" id="txt_categoria" />

        <input type="hidden" id="txt_fecha" />

        <div class="row" id="Reg_Datos">
            <%--ESTA ES LA PARTE DE IZQUIERDA DONDE SE ENCUENTRA EL FOLIO --%>
            <%--<img src="../../Recursos/Imagenes/Logos/reducida.png" />--%>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label labeles">Folio</label>
                <div class="col-lg-3">
                    <input type='text' class="form-control labeles" id="txt_folio" disabled="disabled" />
                </div>
                <div class="col-lg-1"></div>
                <label for="example-text-input" class="col-2 col-form-label labeles">Total</label>
                <div class="col-lg-4">
                    <input type="text" id="txt_total" class="form-control labeles" placeholder="$0" disabled="disabled" />
                </div>
            </div>
            <hr />
            <br />
            <div class="col-xs-12 col-md-12">
                <table id="Tbl_Registros" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>
            <br />
            <hr />
            <div class="form-group row">
                <div class="col-lg-6">
                    <button id="btn_imprimir" type="button" class="btn btn-info btn_pendiente" title="Hacer Venta"><i class="fa fa-print"></i>&nbsp;&nbsp;Registrar Venta</button>
                </div>
                <div class="col-lg-6">
                    <button id="Btn_Limpiar_Todo" type="button" class="btn btn-success btn_pendiente" title="Limpiar"><i class="fa fa-eraser"></i>&nbsp;&nbsp;Limpiar&nbsp;</button>
                </div>
            </div>
        </div>

        <div class="row" id="cont_alta">
            <div class="form-group row select">
                <div class="col-10">
                    <select id="cmb_categoria" class="labeles" onchange="Cargar_Cmb_Producto()"></select>
                </div>
            </div>
            <br />
            <div class="form-group row select">
                <div class="col-10">
                    <select id="cmb_producto" class="form-control"  onkeypress="$('#txt_cantidad').focus().select()" oncuechange="$('#txt_cantidad').focus().select()" onchange="$('#txt_cantidad').focus().select()"></select>
                </div>
            </div>

            <div id="Cantidad">
                <button id="por" type="button" class="btn btn-info btn_x" title="X"><i></i>&nbsp;X&nbsp;</button>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_1" type="button" class="btn btn-info btn_principal_ventas" title="1"><i class="glyphicon glyphicon-print"></i>&nbsp;1&nbsp;</button>
                    <button id="btn_2" type="button" class="btn btn-info btn_principal_ventas" title="2"><i class="glyphicon glyphicon-print"></i>&nbsp;2&nbsp;</button>
                    <button id="btn_3" type="button" class="btn btn-info btn_principal_ventas" title="3"><i class="glyphicon glyphicon-print"></i>&nbsp;3&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_4" type="button" class="btn btn-info btn_principal_ventas" title="4"><i class="glyphicon glyphicon-print"></i>&nbsp;4&nbsp;</button>
                    <button id="btn_5" type="button" class="btn btn-info btn_principal_ventas" title="5"><i class="glyphicon glyphicon-print"></i>&nbsp;5&nbsp;</button>
                    <button id="btn_6" type="button" class="btn btn-info btn_principal_ventas" title="6"><i class="glyphicon glyphicon-print"></i>&nbsp;6&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_7" type="button" class="btn btn-info btn_principal_ventas" title="7"><i class="glyphicon glyphicon-print"></i>&nbsp;7&nbsp;</button>
                    <button id="btn_8" type="button" class="btn btn-info btn_principal_ventas" title="8"><i class="glyphicon glyphicon-print"></i>&nbsp;8&nbsp;</button>
                    <button id="btn_9" type="button" class="btn btn-info btn_principal_ventas" title="9"><i class="glyphicon glyphicon-print"></i>&nbsp;9&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_ce" type="button" class="btn btn-info btn_principal_ventas" title="CE"><i class="glyphicon glyphicon-print"></i>&nbsp;CE&nbsp;</button>
                    <button id="btn_0" type="button" class="btn btn-info btn_principal_ventas" title="0"><i class="glyphicon glyphicon-print"></i>&nbsp;0&nbsp;</button>
                    <button id="btn_punto" type="button" class="btn btn-info btn_principal_ventas" title="."><i class="glyphicon glyphicon-print"></i>&nbsp;.&nbsp;</button>
                </div>

            </div>
            <div id="Precio">
                <button id="por_" type="button" class="btn btn-info btn_x" title="X"><i></i>&nbsp;X&nbsp;</button>

                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_1_p" type="button" class="btn btn-info btn_principal_ventas" title="1"><i class="glyphicon glyphicon-print"></i>&nbsp;1&nbsp;</button>
                    <button id="btn_2_p" type="button" class="btn btn-info btn_principal_ventas" title="2"><i class="glyphicon glyphicon-print"></i>&nbsp;2&nbsp;</button>
                    <button id="btn_3_p" type="button" class="btn btn-info btn_principal_ventas" title="3"><i class="glyphicon glyphicon-print"></i>&nbsp;3&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_4_p" type="button" class="btn btn-info btn_principal_ventas" title="4"><i class="glyphicon glyphicon-print"></i>&nbsp;4&nbsp;</button>
                    <button id="btn_5_p" type="button" class="btn btn-info btn_principal_ventas" title="5"><i class="glyphicon glyphicon-print"></i>&nbsp;5&nbsp;</button>
                    <button id="btn_6_p" type="button" class="btn btn-info btn_principal_ventas" title="6"><i class="glyphicon glyphicon-print"></i>&nbsp;6&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_7_p" type="button" class="btn btn-info btn_principal_ventas" title="7"><i class="glyphicon glyphicon-print"></i>&nbsp;7&nbsp;</button>
                    <button id="btn_8_p" type="button" class="btn btn-info btn_principal_ventas" title="8"><i class="glyphicon glyphicon-print"></i>&nbsp;8&nbsp;</button>
                    <button id="btn_9_p" type="button" class="btn btn-info btn_principal_ventas" title="9"><i class="glyphicon glyphicon-print"></i>&nbsp;9&nbsp;</button>
                </div>
                <div class="form-group row">
                    <div class="col-lg-3"></div>
                    <button id="btn_ce_p" type="button" class="btn btn-info btn_principal_ventas" title="CE"><i class="glyphicon glyphicon-print"></i>&nbsp;CE&nbsp;</button>
                    <button id="btn_0_p" type="button" class="btn btn-info btn_principal_ventas" title="0"><i class="glyphicon glyphicon-print"></i>&nbsp;0&nbsp;</button>
                    <button id="btn_punto_p" type="button" class="btn btn-info btn_principal_ventas" title="."><i class="glyphicon glyphicon-print"></i>&nbsp;.&nbsp;</button>
                </div>
            </div>

            <div class="form-group row">

                <label for="example-text-input" class="col-2 col-form-label labeles">Cantidad</label>
                <div class="col-4">
                    <input type="text" id="txt_cantidad" class="form-control labeles" placeholder="Cantidad" aria-describedby="basic-addon1" />
                </div>

                <label for="example-text-input" class="col-2 col-form-label labeles">Precio</label>
                <div class="col-4">
                    <input type="text" id="txt_precio" class="form-control labeles" placeholder="Precio" aria-describedby="basic-addon1" />
                </div>
            </div>
            <div class="form-group row">
                <label for="example-text-input" class="col-2 col-form-label labeles">Importe</label>
                <div class="col-4">
                    <input type="text" id="txt_importe" class="form-control labeles" disabled="disabled" placeholder="$0" aria-describedby="basic-addon1" />
                </div>
                <div class="col-lg-6">
                    <button id="btn_agregar" type="button" class="btn btn-success btn_my_agregar" title="Agregar" tabindex="4"><i class="fa fa-plus"></i>&nbsp;&nbsp;</button>
                </div>
            </div>
        </div>

        <div id="Ventana_Espera" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Procesando</h4>
                    </div>
                    <div class="modal-body">
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped active" role="progressbar"
                                aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="progressBackgroundFilter" class="progressBackgroundFilter">
            <div class="processMessage" id="div_progress">
            </div>
        </div>

    </div>

</asp:Content>

