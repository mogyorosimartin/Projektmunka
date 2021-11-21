/// <reference path='babylon.d.ts' />

const canvas = document.getElementById('renderCanvas');
const engine = new BABYLON.Engine(canvas, true);
var container = [];


function createCamera(scene){
    const camera = new BABYLON.ArcRotateCamera('camera',0,0,0,new BABYLON.Vector3.Zero,scene);
    camera.attachControl(canvas);
    camera.lowerRadiusLimit=6;
    camera.upperRadiusLimit=20;
    camera.wheelPrecision=20;     
    camera.position = new BABYLON.Vector3(5,10, 15);   

}
function createLight(scene){
    const light=new BABYLON.PointLight('light', new BABYLON.Vector3(0,10,10),scene);
    light.intensity=.1;  
    //light.groundColor=new BABYLON.Color3(0,0,1);
    

}
function createRing(scene,ringmodel,color1,color2){     
    
    if(container.length!=0)
    {
        container.forEach(mesh=>{
            mesh.dispose();           
            container=[];
        })
    }    
    BABYLON.SceneLoader.ImportMesh(null,`/assets/rings/${ringmodel}/`,'scene.GLTF',scene, (meshes)=>{
       // console.log(meshes); 
        meshes.forEach((mesh)=>{
            container.push(mesh);
            if(mesh.material!=null && color2!=null &&color2!="")
            {
            if(mesh.material.id=="Gem_1" ||mesh.material.id=="Gem_2")
            {
                //mesh.materials[0].pbrMetallicRoughness.baseColorFactor[0]=0;
                console.log(mesh.material);
                mesh.material._albedoColor.r=color2.r;
                console.log(color2.r);
                mesh.material._albedoColor.g=color2.g;
                console.log(color2.g);
                mesh.material._albedoColor.b=color2.b;
                console.log(color2.b);

            }
            }
                if(mesh.material!=null && color1!=null && color1!="")
                {
                if(mesh.material.id=="Metal_1" ||mesh.material.id=="Metal_2")
                {
                    //mesh.materials[0].pbrMetallicRoughness.baseColorFactor[0]=0;
                    console.log(mesh.material);
                    mesh.material._albedoColor.r=color1.r;
                    console.log(color1.r);
                    mesh.material._albedoColor.g=color1.g;
                    console.log(color1.g);
                    mesh.material._albedoColor.b=color1.b;
                    console.log(color1.b);

                }
                }
             
        } 
                  
    );
    });
   
   
}

function createSkybox(scene)
{
    const skybox=BABYLON.MeshBuilder.CreateBox('skybox', { size:1000},scene);
    const skyboxMaterial=new BABYLON.StandardMaterial('skyboxMaterail',scene);
    skyboxMaterial.backFaceCulling=false;
    //skyboxMaterial.specularColor=BABYLON.Color3.Black();
    //skyboxMaterial.diffuseColor=BABYLON.Color3.Black();
    skyboxMaterial.reflectionTexture=new BABYLON.Texture("assets/skybox/basicskybox.jpg",scene);
    skyboxMaterial.reflectionTexture.coordinatesMode=BABYLON.Texture.PROJECTION_MODE;
    skybox.material=skyboxMaterial;
    skybox.infiniteDistance=true;

    
}
function createScene()
 {
     
    const scene=new BABYLON.Scene(engine);    
    scene.ambientColor  = BABYLON.Color3.White();  
    var container = new BABYLON.AssetContainer(scene); 
    var current="ring1";
    var currentcolors=[new BABYLON.Color3(0.75294117647,0.75294117647,0.75294117647),new BABYLON.Color3.White()] 
    createCamera();
    createLight(scene);   
    createRing(scene,current); 
    createSkybox(scene);    
    document.getElementById('button').onclick=function(){
        createRing(scene,"ring1"); 
        current="ring1";       
    };  
    document.getElementById('button1').onclick=function(){
        createRing(scene,"ring2"); 
        current="ring2";       
    };  
    document.getElementById('button2').onclick=function(){      
       currentcolors[0]= new BABYLON.Color3(0.74901960784,0.60784313725,0.18823529411);   
       createRing(scene,current,currentcolors[0],currentcolors[1]);   
    };  
    document.getElementById('button3').onclick=function(){
        currentcolors[0]=new BABYLON.Color3(0.75294117647,0.75294117647,0.75294117647)
        createRing(scene,current,currentcolors[0],currentcolors[1]);       
     };
     document.getElementById('button4').onclick=function(){
        currentcolors[1]=new BABYLON.Color3.Red()
        createRing(scene,current,currentcolors[0],currentcolors[1]);

     };
     document.getElementById('button5').onclick=function(){
        currentcolors[1]=new BABYLON.Color3.Blue()
        createRing(scene,current,currentcolors[0],currentcolors[1]);       
     };
     document.getElementById('button6').onclick=function(){
        currentcolors[1]=new BABYLON.Color3.White()
        createRing(scene,current,currentcolors[0],currentcolors[1]);      
     };
    scene.createDefaultEnvironment();
    scene.environmentTexture=new BABYLON.HDRCubeTexture("assets/skybox/parking.hdr",scene,128, false, true, false, true);    
    return scene;
 }
 
 const mainScene=createScene();

 engine.runRenderLoop(()=>{
     mainScene.render();
     
 })