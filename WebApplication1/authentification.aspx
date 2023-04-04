<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="authentification.aspx.cs" Inherits="CSA_Clementine1.authentification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <!-- Required meta tags -->
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title>EGBS</title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="assets/vendors/flag-icon-css/css/flag-icon.min.css">
    <link rel="stylesheet" href="assets/vendors/css/vendor.bundle.base.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <link rel="stylesheet" href="./assets/vendors/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="./assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.css" />
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="./assets/css/demo_1/style.css" />
    <!-- endinject -->
    <link rel="shortcut icon" href="./assets/images/favicon.png" />
</head>
<body style="background-image:url('assets/images/body-bg.png');">
    <form runat="server">

        <div class="row">

            <div class="col-md-4" style="margin: auto; padding-top: 10%;">
                <div class="card">
                    <div class="card-body">
                        <h2>EGBS</h2>
                        <div>
                            <input runat="server" id="txtLogin" type="text" class="form-control" placeholder="Identifiant" />
                            <br />
                            <input runat="server" id="txtMdp" type="password" class="form-control" placeholder="Mot de passe" />
                            <br />
                            <div class="center">
                                <p>Entrez votre identifiant et votre mot de passe</p>
                            </div>
                            <button  style="width:100%;" class="btn btn-primary" type="submit" runat="server" id="btn_connect" onserverclick="btn_connect_ServerClick"><i class="mdi mdi-lock-open"></i>S'identifier</button>
                        </div>
                    </div>
                </div>

            </div>
        </div>


                 <!-- Modal Error Message -->
    <button class="btn btn-danger" data-toggle="modal" data-target="#modalError" type="button" id="btnError" style="display:none">modalError</button>
        <!-- Modal -->
    <div class="modal fade" id="modalError" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">ATTENTION</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:Label Text="test" ID="lblMsg" runat="server" />                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Retour</button>                    
                </div>
            </div>
        </div>
    </div>


    </form>



    <!-- plugins:js -->
    <script src="assets/vendors/js/vendor.bundle.base.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src="assets/js/off-canvas.js"></script>
    <script src="assets/js/hoverable-collapse.js"></script>
    <script src="assets/js/misc.js"></script>
    <script src="assets/js/settings.js"></script>
    <script src="assets/js/todolist.js"></script>
    <!-- endinject -->
    <!-- Custom js for this page -->
    <!-- End custom js for this page -->

    <!-- Modal Error Message -->
    <asp:Literal Text="" runat="server" ID="srcpt1" />
</body>
</html>
