<svelte:head>
	<title>Create a new customer</title>
	<meta name="description" content="Create a new customer" />
</svelte:head>

<div>
	<form method="POST">
		<h2>Create a new customer</h2>
		<label>
			First name
			<input required name="firstName" />
		</label>
		<label>
			Last name
			<input required name="lastName" />
		</label>
		<button>Create customer</button>
	</form>
</div>

<style>
	h2 {
		font-size: 2em;
		font-weight: bold;
	}
	form {
		display: flex;
		flex-direction: column;
		background-color: white;
		border-radius: 8px;
		box-shadow: 4px 4px 5px;
		padding: 1em;
		gap: 1em;
	}

	label {
		display: flex;
		flex-direction: column;
		gap: 1em;
		font-weight: bold;
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
		margin-left: auto;
	}
</style>
