'use client';

import Countdown, { zeroPad } from "react-countdown"

const renderer = ({days, hours, minutes, seconds, completed}:
	{ 
		days:number, 
		hours:number, 
		minutes:number, 
		seconds:number, 
		completed: boolean
	}) => {
	
	return (
		<div className={`boder-2 border-white text-white py-1 rounded-lg flex justify-center 
			${completed ? 'bg-red-600': (days == 0 && hours < 10) ? 'gb-amber-600': 'gb-gree-600'} 
		`}>
			{ completed ? (
				<span>Auction finised</span>
				) : (<span suppressHydrationWarning>
					{days}{zeroPad (hours)}:{zeroPad(minutes)}:{zeroPad(seconds)}</span>) 
			}
		</div>
	);

}

type Props = {
	auctionEnd: string;
}

export default function CountdownTimer({auctionEnd}: Props) {
	return (
		<div>
			<Countdown date={auctionEnd} renderer={renderer} />
		</div>
	)
}
