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

	<table>
		<thead>
			<tr>
				<th>Name</th>
				<th>Surname</th>
				<th>Balance</th>
				<th>Transactions</th>
			</tr>
		</thead>
		<tbody>
			{#each data.customers as customer}
				<tr>
					<td>{customer.firstName}</td>
					<td>{customer.lastName}</td>
					<td>0</td>
					<td><a href={`/customers/${customer.id}`}>View transactions</a></td>
				</tr>
			{/each}
		</tbody>
	</table>

	<a href="/customers">Create a new customer</a>
</section>

<style>
	h2 {
		font-size: 2em;
		font-weight: bold;
	}

	th {
		text-align: left;
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
