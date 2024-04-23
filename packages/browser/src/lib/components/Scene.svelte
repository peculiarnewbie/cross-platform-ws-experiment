<script lang="ts">
	import { T } from '@threlte/core';
	import { OrbitControls } from '@threlte/extras';
	import { AmbientLight } from 'three';

	export let isMovingUp: boolean;
	export let isMovingDown: boolean;

	let lBoxY = 0;
	export let rBoxY = 0;

	const update = setInterval(() => {
		if (isMovingUp) lBoxY += 0.1;
		else if (isMovingDown) lBoxY -= 0.1;
	});
</script>

<T.PerspectiveCamera makeDefault position={[0, 0, 40]} fov={35}>
	<OrbitControls rotateSpeed={0.1} enableDamping target={[0, 0, 0]} />
</T.PerspectiveCamera>

<T.DirectionalLight intensity={1.8} position={[0, 10, 10]} castShadow shadow.bias={-0.0001} />
<T.AmbientLight intensity={1} position={[10, 10, 10]} />

<T.Mesh position={[-5, lBoxY, 0]}>
	<T.BoxGeometry />
	<T.MeshStandardMaterial />
</T.Mesh>

<T.Mesh position={[5, rBoxY, 0]}>
	<T.BoxGeometry />
	<T.MeshStandardMaterial />
</T.Mesh>
