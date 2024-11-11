<script lang="ts">
	import type { PageData } from './$types';

	let { data }: { data: PageData } = $props();
</script>

<svelte:head>
	<title>D♭ - Customer account management</title>
	<meta name="description" content="Manage customer accounts" />
</svelte:head>

<section>
	<h2>Customers</h2>

	{#each data.customers as customer}
		<div>{customer?.firstName}</div>
		<div>{customer?.lastName}</div>
	{/each}

	<a href="/customers">Create a new customer</a>
</section>

<style>
	section {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		flex: 0.6;
	}
</style>
