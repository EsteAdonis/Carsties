'user client';

import { Dropdown, DropdownDivider, DropdownItem } from "flowbite-react";
import { User } from "next-auth";
import Link from "next/link";
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from "react-icons/ai";
import { HiCog, HiUser } from "react-icons/hi";

type Props = {
	user: User;
}

export default function UserActions({ user }: Props) {
	function singOut(arg0: { redirectTo: string; }): void {
		throw new Error("Function not implemented.");
	}

	return (
		<Dropdown inline label={`Welcome ${user.name}`} className="cursor-pointer">
			<DropdownItem icon={HiUser}>
				My Auctions
			</DropdownItem>
			<DropdownItem icon={AiFillTrophy}>
				Actions won
			</DropdownItem>
			<DropdownItem icon={AiFillCar}>
				Sell my car
			</DropdownItem>
			<DropdownItem icon={HiCog}>
				<Link href='/session'>
					Session (dev only!)
				</Link>
			</DropdownItem>
			<DropdownDivider />
			<DropdownItem icon={AiOutlineLogout} onClick={()=> singOut({redirectTo: '/'})}>
				Sing Out.
			</DropdownItem>
		</Dropdown>
	)
}
