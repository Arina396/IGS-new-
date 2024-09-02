import * as THREE from 'three';

const width = 300, height = 300;
const camera = new THREE.PerspectiveCamera( 70, width / height, 0.01, 10 );
camera.position.z = 1;
const scene = new THREE.Scene();
const geometry =new THREE.BoxGeometry()
const material = new THREE.MeshBasicMaterial({color: 0x00FF22});
const mesh = new THREE.Mesh( geometry, material );
mesh.scale.set(0.5,0.5,0.5)
scene.add( mesh );
const renderer = new THREE.WebGLRenderer( { antialias: true } );
renderer.setSize( width, height );
renderer.setAnimationLoop( animation );
document.body.appendChild( renderer.domElement );
function animation( time ) {

	mesh.rotation.x = time / 2000;
	mesh.rotation.y = time / 1000;

	renderer.render( scene, camera );

}