import { Button } from "@/components/ui/button"
import Link from "next/link"

export default function Home() {
  return (
    <div>
      <Button>
        <Link href={"/properties"}>Go to Properties</Link>
      </Button>
    </div>
  )
}
