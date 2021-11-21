/// <reference path='babylon.d.ts' />

const canvas = document.getElementById('renderCanvas');
const engine = new BABYLON.Engine(canvas, true);
function createCamera(scene){
    const camera = new BABYLON.ArcRotateCamera('camera',0,0,0,new BABYLON.Vector3.Zero,scene);
    camera.attachControl(canvas);
    camera.lowerRadiusLimit=6;
    camera.upperRadiusLimit=20;
    camera.wheelPrecision=20;     
    camera.position = new BABYLON.Vector3(5,10, 15);   

}
function createLight(scene){
    const light=new BABYLON.HemisphericLight('light', new BABYLON.Vector3(0,0,1),scene);
    light.intensity=1;  
    const light1=new BABYLON.HemisphericLight('light1', new BABYLON.Vector3(0,1,0),scene);
    light.intensity=1;
    const light2=new BABYLON.HemisphericLight('light2', new BABYLON.Vector3(1,0,0),scene);
    light.intensity=1; 

}
function createRing(scene){ 
       
    BABYLON.SceneLoader.ImportMesh(null,'/assets/rings/RING1/','scene.glb',scene, (meshes)=>{
        console.log(meshes);        
    });

   
}
function createSkybox(scene)
{
    const skybox=BABYLON.MeshBuilder.CreateBox('skybox', { size:1000},scene);
    const skyboxMaterial=new BABYLON.StandardMaterial('skyboxMaterail',scene);
    skyboxMaterial.backFaceCulling=false;
    skyboxMaterial.specularColor=BABYLON.Color3.Black();
    skyboxMaterial.diffuseColor=BABYLON.Color3.Black();
    skyboxMaterial.reflectionTexture=new BABYLON.Texture("assets/skybox/basicskybox.jpg",scene);
    skyboxMaterial.reflectionTexture.coordinatesMode=BABYLON.Texture.PROJECTION_MODE;
    skybox.material=skyboxMaterial;
    skybox.infiniteDistance=true;

    
}
function createScene()
 {
     
    const scene=new BABYLON.Scene(engine);
    scene.clearColor = BABYLON.Color3.Gray();
    createCamera();
    createLight(scene);   
    createRing(scene); 
    createSkybox(scene);
    
    return scene;
 }

 const mainScene=createScene();

 engine.runRenderLoop(()=>{
     mainScene.render();
 })