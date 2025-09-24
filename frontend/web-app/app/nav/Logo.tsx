'use client';
import { useParamsStore } from '@/hooks/useParamsStore'
import { usePathname } from 'next/navigation';
import { useRouter } from 'next/router';
import { AiOutlineCar } from 'react-icons/ai'

export default function Logo() {
	const router = useRouter();
	const pathname = usePathname();

	const reset = useParamsStore(state => state.reset);
	
	function handleReset() {
		if (pathname !== '/') router.push('/');
		reset();
	}

	return (
		<div 
			onClick={handleReset}
			className="flex items-center gap-2 text3xl font-semibold text-red-500">
			<AiOutlineCar size={24}/>
			<div>Carsties Auctions</div>
		</div>
	)
}
