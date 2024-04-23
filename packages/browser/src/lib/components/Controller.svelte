<script lang="ts">
	import { onMount } from 'svelte';

	export let isMovingUp = false;
	export let isMovingDown = false;

	export let move: (msg: string) => void;

	const handleKeyDown = (event: KeyboardEvent) => {
		const key = event.key;
		if (key === 'w') {
			if (isMovingUp) return;
			isMovingUp = true;
			ws.send('up');
		} else if (key === 's') {
			if (isMovingDown) return;
			isMovingDown = true;
			ws.send('dn');
		}
	};

	const handleKeyUp = (event: KeyboardEvent) => {
		const key = event.key;
		if (key === 'w') {
			if (!isMovingUp) return;
			isMovingUp = false;
			ws.send('su');
		} else if (key === 's') {
			if (!isMovingDown) return;
			isMovingDown = false;
			ws.send('sd');
		}
	};

	let ws: WebSocket;

	onMount(() => {
		if (ws) return;
		ws = new WebSocket('wss://unity-cf-relay.peculiarnewbie.workers.dev/api/room/hecc/websocket');
		ws.onmessage = (msg) => {
			move(msg.data);
		};
	});
</script>

<svelte:document on:keydown={handleKeyDown} on:keyup={handleKeyUp} />

<div>{isMovingUp} {isMovingDown}</div>
