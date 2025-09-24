import getCurrentUser from "../actions/authActions"
import LoginButton from "./LoginButton"
import Logo from "./Logo"
import Search from "./Search"
import UserActions from "./UserActions";

export default async function NavBar() {
	const user = await getCurrentUser();
	return (
		<header className="sticky top-0 z-50 flex justify-between bg-black p-4 items-center text-gray-400 shadow-amber-500">
			<Logo />
			<Search />
			{user ? (<UserActions />) : (<LoginButton />)}
			
		</header>
	)
}
