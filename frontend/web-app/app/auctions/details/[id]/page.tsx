import React from 'react'
import { getDetailedViewdata} from '@/app/actions/auctionActions';
import Heading from '@/app/components/Heading';
import CountDownTimer from '../../CountdownTimer';
import CarImage from '../../CarImage';
import DetailedSpecs from './DetailedSpecs';

export default async function details({params}: {params: Promise<{id: string}>}) {
	const {id} = await params;
	const data = await getDetailedViewdata(id);

	return (
		<>
			<div className="flex justify-between">
				<Heading title={`${data.make} ${data.model}`} />
				<div className="flex gap-3">
					<h3 className="text-2x1 font-semibold">Time remaining:</h3>
					<CountDownTimer auctionEnd={data.auctionEnd}></CountDownTimer>
				</div>
			</div>

			<div className="gird grid-cols-2 gap-6 mt-3">
				<div className="relative w-full bg-gray-200 aspect-[4/3] rounded-lg overflow-hidden">
					<CarImage imageUrl={data.imageUrl} />
				</div>
				<div className="border-2 rounded-lg p-2 bg-gray-200">
					<Heading title="Bids" />
				</div>
				<div className="mt-3 grid gird-cols-1 rounded-lg">
					<DetailedSpecs auction={data}/>
				</div>
			</div>
		</>
	)
}
