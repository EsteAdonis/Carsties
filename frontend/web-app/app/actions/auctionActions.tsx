'use server';
import {PagedResult, Auction} from "@/types";

export async function getDate(query: string): Promise<PagedResult<Auction>> {
	const res = await 
	fetch(`http://localhost:6001/search${query}`);

	if(!res.ok) throw new Error('Failed to fetch data');

	return res.json();
}