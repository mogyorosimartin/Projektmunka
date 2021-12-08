<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="User_Login_CS.Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="user-scalable=0, initial-scale=1, minium-scale=1, maximum-scale=1, width=device-width, minimal-ui=1" />
    <title>Projektmunka Gyűrű 3D</title>
    <link rel="stylesheet" type="text/css" href="index.css" />
</head>
<body>
    <canvas id="renderCanvas"></canvas>
    <script type="application/javascript" src="https://preview.babylonjs.com/babylon.js"></script>
    <script type="application/javascript" src="https://preview.babylonjs.com/inspector/babylon.inspector.bundle.js"></script>
    <script type="application/javascript" src="https://preview.babylonjs.com/loaders/babylonjs.loaders.min.js"></script>
    <script type="application/javascript" src="https://preview.babylonjs.com/materialsLibrary/babylonjs.materials.min.js"></script>
    <script type="application/javascript" src="https://preview.babylonjs.com/postProcessesLibrary/babylonjs.postProcess.min.js"></script>
    <script type="application/javascript" src="https://preview.babylonjs.com/proceduralTexturesLibrary/babylonjs.proceduralTextures.min.js"></script>
    <script type="application/javascript" src="https://preview.babylonjs.com/serializers/babylonjs.serializers.min.js"></script> 
    <script src="main.js" type="application/javascript"></script>
    <div class="UI"><form id="formSave" runat="server" action="Home.aspx">
        <div class="login">
            <div>
                <div id="LoginID1">
                <h1>Üdvözöllek 
                <asp:LoginName ID="LoginName1" runat="server" Font-Bold="true" /></h1>
                <asp:Label ID="lblLastLoginDate" runat="server" />
                <asp:LoginStatus ID="LoginStatus1" runat="server" />
                </div>
                <div id="UI1">
                <br />
                <h1>Ékszer mentése</h1>                
                <asp:TextBox ID="TextBox_JewName" placeholder="Mentés neve" runat="server"></asp:TextBox> <br />
                <asp:Label ID="FailureSave" Text="" runat="server"></asp:Label>
                <asp:HiddenField id="color1r" runat="server" value="1"/>
                <asp:HiddenField id="color1g" runat="server" value="1"/>
                <asp:HiddenField id="color1b" runat="server" value="1"/>
                <asp:HiddenField id="color2r" runat="server" value="1"/>
                <asp:HiddenField id="color2g" runat="server" value="1"/>
                <asp:HiddenField id="color2b" runat="server" value="1"/>
                <asp:HiddenField id="model" runat="server" value="1"/>
                <asp:Button ID="Save" runat="server" Text="Jelenlegi ékszer mentése" OnClick="Save_Click"/><br />                 
                <h1>Mentett ékszerek</h1>
                <asp:Label ID="Failure" Text="" runat="server"></asp:Label><br />
                </div>
                <div id="Table1">
                <asp:GridView ID="GridView1" OnSorting="SortRecords" runat="server"
                     AllowSorting="True" DataKeyNames="id" 
                     AllowPaging="True" OnPageIndexChanging="PaginateGridView" 
                     PageSize="5" PagerSettings-Mode="Numeric"
                     EnablePersistedSelection="True" AutoGenerateColumns="False">
                 <Columns >
                     <asp:TemplateField HeaderText="Neve" SortExpression="name">
                        <ItemTemplate>
                            <asp:Label ID="Label2"  runat="server" Text='<%# Bind("name") %>'></asp:Label>
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Betöltés">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" CssClass="HyperLink1" Text="✓" runat="server" NavigateUrl='<%#String.Format("javascript:createRing(scene,\"{0}\",new BABYLON.Color3({1},{2},{3}),new BABYLON.Color3({4},{5},{6}));", Eval("model"), (Eval("color1-r").ToString()).Replace(",","."), (Eval("color1-g").ToString()).Replace(",","."), (Eval("color1-b").ToString()).Replace(",","."), (Eval("color2-r").ToString()).Replace(",","."), (Eval("color2-g").ToString()).Replace(",","."), (Eval("color2-b").ToString()).Replace(",","."))%>'></asp:HyperLink>
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="Törlés">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" CssClass="HyperLink2" Text="X" runat="server" NavigateUrl='<%#String.Format("Home.aspx?deleteID={0}", Eval("id"))%>'></asp:HyperLink>
                        </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
               </div> 
            </div>
            
        </div>
                  
        <div class="Rings">
            <div class="Labels">
            <label>Gyűrűk</label> 
            </div>
                <div class="Model">
                    <asp:Label ID="ModelList"  runat="server" Text=""></asp:Label>           
                </div>
            <div class="Metal">
                <div class="Labels">
                <label>Fémek</label><br />
                </div>
                <asp:Label ID="MetalList"  runat="server" Text=""></asp:Label>
            </div>
            <div class="Gems" >
                <div class="Labels">
                <label>Drágakövek</label><br />
                </div>
                <asp:Label ID="GemList"  runat="server" Text=""></asp:Label>           
            </div>
        </div>

         <% if (Is_User_Admin() == true) { %>
            <div class="login">
                <h1>Admin</h1>
                <h3>Model hozzáadás</h3>                
                <asp:TextBox ID="TextBox_ModelName" placeholder="Modelnév (assets/rings/*ez*/)" runat="server"></asp:TextBox> <br />
                <asp:TextBox ID="TextBox_ModelImg" placeholder="Model kép útvonal" runat="server"></asp:TextBox> <br />
                <asp:Label ID="Model_Resp" Text="" runat="server"></asp:Label>
                <asp:Button ID="Model_Add" runat="server" Text="Hozzáadás" OnClick="Model_Add_Click"/><br />    
                <h3>Drágakő/Fém hozzáadás</h3>                
                <asp:TextBox ID="TextBox_DFColorr" placeholder="Babylon Red(0-1)" runat="server"></asp:TextBox> <br />
                <asp:TextBox ID="TextBox_DFColorg" placeholder="Babylon Green(0-1)" runat="server"></asp:TextBox> <br />
                <asp:TextBox ID="TextBox_DFColorb" placeholder="Babylon Blue(0-1)" runat="server"></asp:TextBox> <br />
                <asp:TextBox ID="TextBox_DFHexcolor" placeholder="Hexcolor #XXXXXX" runat="server"></asp:TextBox> <br />
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Text ="Drágakő" Value ="1"></asp:ListItem> 
                    <asp:ListItem Text ="Fém" Value ="2"></asp:ListItem> 
                </asp:DropDownList><br />
                <asp:Label ID="GemMat_Resp" Text="" runat="server"></asp:Label>
                <asp:Button ID="GemMat_Add" runat="server" Text="Hozzáadás" OnClick="GemMat_Add_Click"/><br />  
                <h3>Modellek</h3>
                <asp:Label ID="Model_Del" Text="" runat="server"></asp:Label><br />
                
                <div id="Table1">
                    <asp:GridView ID="GridView2" OnSorting="SortRecords_Models" runat="server"
                         AllowSorting="True" DataKeyNames="id" 
                         AllowPaging="True" OnPageIndexChanging="PaginateGridView_Models" 
                         PageSize="5" PagerSettings-Mode="Numeric"
                         EnablePersistedSelection="True" AutoGenerateColumns="False">
                         <Columns >
                             <asp:TemplateField HeaderText="Neve" SortExpression="name">
                                <ItemTemplate>
                                    <asp:Label ID="Label2"  runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Törlés">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" CssClass="HyperLink2" Text="X" runat="server" NavigateUrl='<%#String.Format("Home.aspx?deleteID_Model={0}", Eval("id"))%>'></asp:HyperLink>
                                </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                </div>
                <h3>Fémek</h3>
                <asp:Label ID="Mat_Del" Text="" runat="server"></asp:Label><br />
                
                <div id="Table1">
                    <asp:GridView ID="GridView3" OnSorting="SortRecords_Mat" runat="server"
                         AllowSorting="True" DataKeyNames="id" 
                         AllowPaging="True" OnPageIndexChanging="PaginateGridView_Mat" 
                         PageSize="5" PagerSettings-Mode="Numeric"
                         EnablePersistedSelection="True" AutoGenerateColumns="False">
                         <Columns >
                             <asp:TemplateField HeaderText="Hex" SortExpression="hexcolor">
                                <ItemTemplate>
                                    <asp:Label ID="Label2"  runat="server" Text='<%# Bind("hexcolor") %>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Törlés">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" CssClass="HyperLink2" Text="X" runat="server" NavigateUrl='<%#String.Format("Home.aspx?deleteID_Mat={0}", Eval("id"))%>'></asp:HyperLink>
                                </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                </div>
                <h3>Drágakövek</h3>
                <asp:Label ID="Gem_Del" Text="" runat="server"></asp:Label><br />
                
                <div id="Table1">
                    <asp:GridView ID="GridView4" OnSorting="SortRecords_Gem" runat="server"
                         AllowSorting="True" DataKeyNames="id" 
                         AllowPaging="True" OnPageIndexChanging="PaginateGridView_Gem" 
                         PageSize="5" PagerSettings-Mode="Numeric"
                         EnablePersistedSelection="True" AutoGenerateColumns="False">
                         <Columns >
                             <asp:TemplateField HeaderText="Hex" SortExpression="hexcolor">
                                <ItemTemplate>
                                    <asp:Label ID="Label2"  runat="server" Text='<%# Bind("hexcolor") %>'></asp:Label>
                                </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Törlés">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HyperLink2" CssClass="HyperLink2" Text="X" runat="server" NavigateUrl='<%#String.Format("Home.aspx?deleteID_Gem={0}", Eval("id"))%>'></asp:HyperLink>
                                </ItemTemplate>
                             </asp:TemplateField>
                         </Columns>
                    </asp:GridView>
                </div>
            </div>
         <% } %>
        </form> 
    </div>
</body>
</html>
