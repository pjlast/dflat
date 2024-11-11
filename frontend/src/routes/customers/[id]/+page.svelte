<script lang="ts">
	import type { PageData } from './$types';

	let { data }: { data: PageData } = $props();
</script>

<svelte:head>
	<title>D♭ - Customer account management</title>
	<meta name="description" content="Manage customer accounts" />
</svelte:head>

<section>
	<h2>Customer: {data.customer.firstName}</h2>

	{#each data.accounts as account}
		<h3>Account {account.id} transactions</h3>
		{#if account.transactions.length === 0}
			<span>This account does not yet have any transactions.</span>
		{:else}
			<ul>
				{#each account.transactions as transaction}
					<li>Transaction #{transaction.id}: €{transaction.amount}</li>
				{/each}
			</ul>
		{/if}
	{/each}

	<form method="POST">
		<strong>Open a new account</strong>
		<label>
			Initial credit
			<input required name="initialCredit" type="number" />
		</label>
		<button>Open account</button>
	</form>
</section>

<style>
	h3 {
		font-weight: bold;
		margin-top: 1em;
	}
	h2 {
		font-size: 2em;
		font-weight: bold;
	}

	form {
		display: flex;
		flex-direction: column;
		padding: 1em;
		gap: 1em;
	}

	input {
		border-width: 1px;
		border-color: gray;
		border-radius: 8px;
		padding: 0.5em;
	}

	button {
		border-width: 1px;
		border-color: gray;
		border-radius: 8px;
		padding: 0.5em;
		background-color: #24a0ed;
		width: fit-content;
		color: white;
		margin-right: auto;
	}

	section {
		display: flex;
		flex-direction: column;
		flex: 0.6;
		background-color: white;
		border-radius: 8px;
		padding: 1em;
	}
</style>
