<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="accueil.aspx.cs" Inherits="WebApplication1.accueil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="row">
              <div class="col-xl-12 stretch-card grid-margin">
                <div class="card">
                  <div class="card-body">
                    <div class="d-flex justify-content-between flex-wrap">
                      <div>
                        <div class="card-title mb-0"></div>
                          <h3 class="font-weight-bold mb-0"> TODO List
                    </h3>
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
                      </div>
                      <div>
                        
                      </div>
                    </div>

                  </div>
                </div>
              </div>              
            </div>


       








</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFootHolder2" runat="server">


    <!-- endinject -->
    <!-- Custom js for this page -->
    <script src="./assets/js/dashboard.js"></script>
 
</asp:Content>
