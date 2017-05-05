<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ucwSalida.ascx.vb" Inherits="SMV.Interoperabilidad.Web.ucwSalida" %>

<div class="modal-dialog PopDetSalida">
    <div class="modal-content">
        <div class="modal-header">
            <button aria-hidden="true" data-dismiss="modal" class="close" type="button">×</button>
            <h4 class="modal-title">
                Detalle de la Salida
                <asp:HiddenField ID="hdnDSTipoServ" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hdnDSCodLog" ClientIDMode="Static" runat="server" />
            </h4>
        </div>
        <div class="modal-body">
			<div class="adv-table">
                <div class="divScroll">
                    <table id="tblGrillaSalida" cellspacing="0" width="100%" class="display table table-hover table-bordered">
                        <thead>
                            <tr role="row">
                                <th style="width:20px">N°</th>
                                <th>Detalle Salida</th>
                            </tr>
						</thead>
                        <tbody>
						</tbody>
                    </table>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal"><i class="fa fa-window-close fa-lg"></i> Cerrar </button>
            </div>
        </div>
    </div>
</div>
