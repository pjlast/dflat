import createClient from "openapi-fetch";
import type { paths } from "$lib/clients/customers-api/schema"
import type { PageServerLoad } from "./$types";
import { error } from "@sveltejs/kit";

export const load: PageServerLoad = async () => {
    const client = createClient<paths>({ baseUrl: "http://localhost:5295" });

    const { data, response } = await client.GET("/api/v1/customers");

    if (data === undefined || response.status != 200) {
        return error(500, { message: "Could not fetch customers" });
    }

    return {
        customers: data,
    }
}

