<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="User_Login_CS.Login" %>

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
    <script src="main.js" type="module"></script>
    <div class="UI">
        <div class="login"><form id="form1" runat="server">                               
                <asp:TextBox ID="TextBox_UserName" runat="server"></asp:TextBox> <br />       
                <asp:TextBox ID="TextBox_Password" runat="server"></asp:TextBox><br />
                <asp:Label ID="Failure" Text="" runat="server"></asp:Label><br />
                <asp:Button ID="Login1" runat="server" Text="Login" OnClick="Login1_Click" />
                <asp:Button ID="Register1" runat="server" Text="Register" OnClick="Register1_Click" /><br /> 
        </form></div>
                  
        <div class="Rings">
            <div class="Labels">
            <label>Gyűrűk</label> 
            </div>
                <div class="Model">
                <button id="button" type="button"> <img src="assets/rings/RING1/RING1.png" width="100%" height="100%" alt="RING1" /></button>
                <button id="button1" type="button"><img src="assets/rings/RING2/RING2.png" width="100%" height="100%" alt="RING2"/></button>
            </div>
            <div class="Metal">
                <div class="Labels">
                <label>Fémek</label><br />
                </div>
                <button id="button2" type="button" style="background-color: goldenrod;"></button>
                <button id="button7" type="button" style="background-color: rosybrown;"></button>
                <button id="button3" type="button" style="background-color: silver;"></button>
            </div>
            <div class="Gems" >
                <div class="Labels">
                <label>Drágakövek</label><br />
                </div>
                <button id="button4" type="button" style="background-image: url(assets/rings/gems/RED.png); background-size:cover;"></button>
                <button id="button5" type="button" style="background-image: url(assets/rings/gems/BLUE.png); background-size:cover;"></button>
                <button id="button6" type="button" style="background-image: url(assets/rings/gems/White.png); background-size:cover;"></button> 
                <button id="button8" type="button" style="background-image: url(assets/rings/gems/Green.png); background-size:cover;"></button>                
            </div>
        </div>
    </div>
</body>
</html>
