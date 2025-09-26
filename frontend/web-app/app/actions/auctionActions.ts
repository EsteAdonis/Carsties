'use server';
import { fetchWrapper } from "@/lib/fetchWrapper";
import {PagedResult, Auction} from "@/types";
import { FieldValues } from "react-hook-form";

export async function getDate(query: string): Promise<PagedResult<Auction>> {
	return fetchWrapper.get(`search${query}`)
}

export async function updateActionTest(): Promise<{status: number, message: string}> {
	const data = {
		mileage: Math.floor(Math.random() * 10000) + 1
	}

	return fetchWrapper.put('auctions/afbee524-5972-4075-8800-7d1f9d7b0a0c', data);
}

export async function createAuction(data: FieldValues) {
	return fetchWrapper.post('auctions', data);
}