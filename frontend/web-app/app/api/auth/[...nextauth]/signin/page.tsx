import EmptyFilter from "@/app/components/EmptyFilter";
import React from 'react';

export default function SignIn({searchParams}: {searchParams: {callbackUrl: string}}) {
	return (
		<EmptyFilter 
			title="You need to be logged in to do that"
			subtitle="Plase click below to login"
			showLogin
			callbackUrl={searchParams.callbackUrl}
		/>
	)
}
