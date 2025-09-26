import { getDetailedViewdata } from '@/app/actions/auctionActions';
import Heading from '@/app/components/Heading';
import React from 'react'
import AuctionForm from '../../AuctionForm';

export default async function Update({params}: {params: Promise<{id: string}>}) {
	const {id} = await params;
	const data = await getDetailedViewdata(id);
	
	return (
		<div className='max-auto max-w-[75%] shadow-lg p-10 bg-white rounded-lg'>
			<Heading title="Please update the details of your car (only these auction properties can be updated)" />
			<AuctionForm auction={data}/>
		</div>
	)
}
