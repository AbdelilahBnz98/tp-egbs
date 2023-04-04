<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Todolist.aspx.cs" Inherits="WebApplication1.Todolist" %>

<!DOCTYPE html>


<div class="page-header">
    <head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <title>EGBS</title>
    <!-- Favicon icon -->
    <link rel="icon" type="image/png" sizes="16x16" href="./images/favicon.png" />
    <link rel="stylesheet" href="./menu/vendor/owl-carousel/css/owl.carousel.min.css" />
    <link rel="stylesheet" href="./menu/vendor/owl-carousel/css/owl.theme.default.min.css" />
    <link href="./menu/vendor/jqvmap/css/jqvmap.min.css" rel="stylesheet" />
    <link href="./menu/css/style.css" rel="stylesheet" />
        </head>
        <nav aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="#">Accueil</a></li>
                <li class="breadcrumb-item active" aria-current="page">TODOList</li>
            </ol>
        </nav>
    </div>
 <div class="row">
        <div class="col-sm-6">
            <div class="card">
                <div class="card-header">
                    <h3>Liste utilisateurs </h3>
                </div>
                <div class="card-body" style="overflow-y: scroll;max-height:350px;">
                    <div class="col-sm-12">
                        <asp:GridView PagerSettings-Mode="NumericFirstLast" runat="server" ID="GridData"
                             AllowSorting="false" AllowPaging="false" AutoGenerateColumns="False"
                            CssClass="table table-bordered">                            
                            <EmptyDataTemplate>
                                Aucune donnée n'a été trouvée.
                            </EmptyDataTemplate>
                            <Columns>
                                <%-- ACTION --%>
                                <asp:TemplateField HeaderText="ACTION">
                                    <ItemTemplate>
                                        <a href='<%# Eval("ref_utilisateur","utilisateurs.aspx?a=c&i={0}")%>' class="btn-sm btn-inverse-primary"><i class="mdi mdi-eye"></i></a>
                                        <a href='<%# Eval("ref_utilisateur","utilisateurs.aspx?a=u&i={0}")%>' class="btn-sm btn-inverse-info"><i class="mdi mdi-pencil"></i></a>
                                        <a href='<%# Eval("ref_utilisateur","utilisateurs.aspx?a=d&i={0}")%>' class="btn-sm btn-inverse-danger"><i class="mdi mdi-trash-can"></i></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- Fonction --%>
                                <asp:TemplateField HeaderText="FONCTION" SortExpression="code_budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%#Eval("libelle_profil")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- identifiant --%>
                                <asp:TemplateField HeaderText="IDENTIFIANT" SortExpression="code_budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdentifiant" runat="server" Text='<%#Eval("login_utilisateur")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- prenom --%>
                                <asp:TemplateField HeaderText="PRENOM" SortExpression="code_budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrenom" runat="server" Text='<%#Eval("prenom_utilisateur")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- nom --%>
                                <asp:TemplateField HeaderText="NOM" SortExpression="code_budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNom" runat="server" Text='<%#Eval("nom_utilisateur")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- email --%>
                                <%--<asp:TemplateField HeaderText="EMAIL" SortExpression="code_budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNom" runat="server" Text='<%#Eval("email_utilisateur")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <%-- ETAT --%>
                                <asp:TemplateField HeaderText="ETAT" SortExpression="code_budget">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEtat" runat="server" Text='<%#Eval("etat_utilisateur")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-6">
            <div class="card">
                <div class="card-header">
                    <h3>Fiche utilisateur</h3>
                </div>
                <div class="card-body">
                    <!-- Fonction -->
                    <div class="row">

                        <div class="col-sm-5 col-form-label">
                            <span class="">Profil</span> <span class="req">*</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:DropDownList runat="server" ID="DropProfil" CssClass="form-control"></asp:DropDownList>
                        </div>

                    </div>
                    <!-- Identifiant -->
                    <div class="row">
                        <div class="col-sm-5 col-form-label">
                            <span class="">Identifiant</span> <span class="req">*</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox runat="server" ID="txtIndentifiant" autocomplete="off" CssClass="form-control" ClientIDMode="Static" />
                        </div>
                    </div>
                    <!-- MDP -->
                    <div class="row">

                        <div class="col-sm-5 col-form-label">
                            <span class="">Mot de passe</span> <span class="req">*</span>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group" style="margin: 0px;">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtMdp" autocomplete="off" CssClass="form-control" ClientIDMode="Static" aria-describedby="basic-addon1" />
                                    <div class="input-group-append">
                                        <span class="input-group-btn">
                                            <asp:CheckBox ID="show_password" runat="server" CssClass="form-check-input" ClientIDMode="Static" />
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <!-- Prénom -->
                    <div class="row">

                        <div class="col-sm-5 col-form-label">
                            <span class="">Prénom</span> <span class="req">*</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox runat="server" ID="txtPrenom" CssClass="form-control" ClientIDMode="Static" />
                        </div>

                    </div>
                    <!-- Nom -->
                    <div class="row">

                        <div class="col-sm-5 col-form-label">
                            <span class="">Nom</span> <span class="req">*</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:TextBox runat="server" ID="txtNom" CssClass="form-control" ClientIDMode="Static" />
                        </div>

                    </div>
                    <!-- email -->
                    <%--<div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-5 col-form-label">
                                <span class="">Email</span> <span class="req">*</span>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                        </div>
                    </div>--%>
                    <!-- ETAT -->
                    <div class="row">
                        <div class="col-sm-5 col-form-label">
                            <span class="">Etat</span> <span class="req">*</span>
                        </div>
                        <div class="col-sm-6">
                            <asp:DropDownList runat="server" ID="DropEtat" CssClass="form-control">
                                <asp:ListItem Text="Activé" Value="A" />
                                <asp:ListItem Text="Désactivé" Value="D" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="card-footer">
                    <div class="row">
                        <div class="col-sm-12">
                            <button id="btnCreer" runat="server" onserverclick="btnCreer_ServerClick" class="btn btn-primary">Créer</button>
                            <button id="btnAskModifier" class="btn btn-primary" runat="server" type="button" data-toggle="modal" data-target="#AskModifier">Modifier</button>
                            <button id="btnAskSupprimer" class="btn btn-primary" runat="server" type="button" data-toggle="modal" data-target="#AskSupprimer">Supprimer</button>
                            <a href="utilisateurs.aspx?a=a&i=0" class="btn btn-secondary">Retour</a>

                            <div style="display: none;">
                                <asp:Button Text="update" ID="btnUpdate" ClientIDMode="Static" OnClick="btnUpdate_Click" runat="server" />
                                <asp:Button Text="supprimer" ID="btnDelete" ClientIDMode="Static" OnClick="btnDelete_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>







    <script type="text/javascript">
        $(document).ready(function () {
            $('#txtMdp').attr('type', $(this).is(':checked') ? 'text' : 'password');
            //CheckBox Show Password  
            $('#show_password').click(function () {
                $('#txtMdp').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });
        });
    </script>



