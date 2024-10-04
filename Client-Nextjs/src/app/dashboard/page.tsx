'use client'

import { usePathname } from "next/navigation";

export default function Dashboard() {
    const pathname = usePathname();
    console.log(pathname);
    return (
        <>
            <div>Dashboard home!</div>
            <button onClick={() => { }}>Click</button>
        </>
    )
}