import React from 'react'

export default async function details({params}: {params: Promise<{id: string}>}) {
	const {id} = await params;
	return (
		<div>Details page: {id}</div>
	)
}
