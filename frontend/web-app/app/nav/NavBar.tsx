import { AiOutlineCar } from "react-icons/ai"

export default function NavBar() {
	return (
		<header className="sticky top-0 z-50 flex justify-between bg-black p-4 items-center text-gray-400 shadow-amber-500">
			<div className="flex items-center gap-2 text3xl font-semibold text-red-500">
				<AiOutlineCar size={24}></AiOutlineCar>
				<div>Carsties Auctions</div>
			</div>
			<div>Search</div>
			<div>Login</div>
		</header>
	)
}
