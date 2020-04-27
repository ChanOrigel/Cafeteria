<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Portal/MasterPage.Master" CodeBehind="Frm_Productos.aspx.cs" Inherits="JPV_Portal.Portal.Frm_Productos" %>


<asp:Content ID="Head" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.1/css/select2.min.css">
    <%--<script src="//code.jquery.com/jquery-1.11.3.min.js"></script>--%>
    <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.1/js/select2.min.js"></script>

    <script src="../JS/Js_Productos.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <label id="txt_showing"></label>
    <section class="container">
        <hr />
        <h2>Productos y Categorias</h2>
        <hr />
    </section>
    <section class="container br-nav">
        <nav class="navbar">
            <div class="d-inline-flex " style="width: 100%;">
                <ul class="nav navbar-nav d-inline-flex">
                    <li class="myClass nav-item">
                        <ul class="list-inline-mb-0">
                            <li class="list-inline-item" id="li-Categ">
                                <div>
                                    <button class="learn-more" id="btn_Categ">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">IR A CATEGORIAS</span>
                                    </button>
                                </div>
                                <%--<button id="btn_Categ" type="button" class="btn btn-warning btn_my_class" title="Categorias"><i class="glyphicon glyphicon-triangle-right"></i>&nbsp;&nbsp;Ir a Categorias</button>--%>
                            </li>
                            <li class="list-inline-item" id="li-Prod" style="display: none">
                                <div>
                                    <button class="learn-more" id="btn_Prod">
                                        <span class="circle" aria-hidden="true">
                                            <span class="icon arrow"></span>
                                        </span>
                                        <span class="button-text">IR A PRODUCTOS</span>
                                    </button>
                                </div>
                                <%--<button id="btn_Prod" type="button" class="btn btn-warning btn_my_class" title="Productos"><i class="glyphicon glyphicon-triangle-left"></i>&nbsp;&nbsp;Ir a Productos</button>--%>
                            </li>
                        </ul>

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
    <hr />
    <div class="container">
        <input type="hidden" id="txt_id" />
        <input type="hidden" id="txt_categ" />
        <input type="hidden" id="txt_categ_id" />

        <div class="row" id="Productos">

            <div class="col-xs-12 col-md-12" id="DatosProd">
                <table id="Tbl_RegistrosProd" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
            </div>

            <div class="row col-12" id="ShowProd" style="display: none;">
                <div class="form-group row">
                    <label class="col-2 col-form-label">*Categoria</label>
                    <div class="col-3">
                        <select id="cmb_Categ" class="form-control" style="width: 100%"></select>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-2 col-form-label">*Descripción</label>
                    <div class="col-3">
                        <input type="text" id="txt_descProd" class="form-control" placeholder="Descripcion" aria-describedby="basic-addon1" />
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-2 col-form-label">*Precio</label>
                    <div class="col-3">
                        <input type="text" id="txt_precio" class="form-control" placeholder="0.00" aria-describedby="basic-addon1" />
                    </div>
                </div>
            </div>


        </div>

        <div class="row" id="Categorias" style="display: none;">

            <div class="row" id="DatosCateg">
                <div class="col-xs-12 col-md-12">
                    <table id="Tbl_RegistrosCateg" class="display compact table-bordered dt-responsive nowrap" cellspacing="0" width="100%"></table>
                </div>
            </div>

            <div class="row col-12" id="ShowCateg" style="display: none;">
                <div class="form-group row">
                    <label for="example-text-input" class="col-2 col-form-label">*Descripción</label>
                    <div class="col-6">
                        <input type="text" id="txt_descCateg" class="form-control" placeholder="Descripcion" aria-describedby="basic-addon1" />
                    </div>
                </div>
              <%--  <div class="form-group row">
                    <label for="example-text-input" class="col-2 col-form-label">*Cargar Imagen</label>
                    <div class="col-6">
                        <input type="file" id="img_file" class="btn btn-info" aria-describedby="basic-addon1" />
                    </div>
                    <div class='preview'>
                        <img src="" id="img" width="100" height="100"/>
                    </div>
                </div>--%>
            </div>

        </div>

    </div>

</asp:Content>
