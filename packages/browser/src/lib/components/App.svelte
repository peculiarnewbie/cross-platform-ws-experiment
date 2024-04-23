<script lang="ts">
	import { Canvas } from '@threlte/core';
	import Scene from './Scene.svelte';
	import Controller from './Controller.svelte';

	let isMovingUp = false;
	let isMovingDown = false;

	let isWSMovingUp = false;
	let isWSMovingDown = false;

	let rBoxY = 0;

	const move = (msg: string) => {
		if (msg === 'up') isWSMovingUp = true;
		else if (msg === 'su') isWSMovingUp = false;
		else if (msg === 'dn') isWSMovingDown = true;
		else if (msg === 'sd') isWSMovingDown = false;
	};

	const update = setInterval(() => {
		if (isWSMovingUp) rBoxY += 0.1;
		else if (isWSMovingDown) rBoxY -= 0.1;
	});
</script>

<div>
	<div style="position: fixed; pointer-events:none">
		<div class="callout">
			<p>press W and S to move</p>
			<Controller bind:isMovingDown bind:isMovingUp {move} />
		</div>
	</div>
	<Canvas>
		<Scene {isMovingDown} {isMovingUp} {rBoxY} />
	</Canvas>
</div>

<style>
	div {
		width: 100%;
		height: 100%;
	}
	p {
		padding: 0;
		margin: 0;
	}
	.callout {
		pointer-events: none;
		font-size: 1.25rem; /* 20px */
		line-height: 1.75rem; /* 28px */
		background-color: rgb(255 255 255 / 0.8);
		width: fit-content;
		height: fit-content;
		text-align: center;
		border-radius: 1rem;
		padding: 0.5rem;
		margin: 1rem;
	}
</style>
