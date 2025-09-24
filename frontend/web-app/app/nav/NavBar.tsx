import LoginButton from "./LoginButton"
import Logo from "./Logo"
import Search from "./Search"

export default function NavBar() {
	return (
		<header className="sticky top-0 z-50 flex justify-between bg-black p-4 items-center text-gray-400 shadow-amber-500">
			<Logo />
			<Search />
			<LoginButton />
		</header>
	)
}
