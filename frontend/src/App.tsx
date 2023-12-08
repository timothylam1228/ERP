import { useState } from "react"
import "./App.css"
import TreeView from "./components/TreeView"
import TreeDataView from "./components/TreeDataView"
import DataGridView from "./components/DataGridView"

export interface Bom {
  children?: Bom[]
  parentName: string | null
  quantity: number
  componentName: string
}
export interface Part {
  name: string
  type: string
  item: string
  title: string
  partNumber: string
  material: string
}
const App = () => {
  const BASE_URL = "http://localhost:5259"
  // Function to recursively build the tree
  const [parts, setParts] = useState<Part[] | []>([])
  const [boms, setBoms] = useState<Bom[] | []>([])
  const [populated, setPopulated] = useState<boolean>(false)
  const [selectedItem, setSelectedItem] = useState<Bom | null>(null)

  const populateData = () => {
    Promise.all([
      fetch(`${BASE_URL}/parts`).then((res) => res.json()),
      fetch(`${BASE_URL}/boms`).then((res) => res.json()),
    ])
      .then(([partsData, bomsData]) => {
        setParts(partsData)
        setBoms(bomsData)
        setPopulated(true)
      })
      .catch((error) => {
        console.error("Failed to fetch data:", error)
        setPopulated(false)
      })
  }

  return (
    <div className=" w-full h-screen gap-4">
      <div className="flex flex-col w-full h-3/4 md:h-1/2 px-6 md:px-12 pb-6 pt-12">
        <div className="flex w-full justify-center py-6">
          <h1 className="text-3xl font-bold">
            Testing Functionality for Tree and Datagrid
          </h1>
        </div>
        <div className="flex flex-col-reverse md:flex-row h-full overflow-auto  ">
          <div className="w-full md:w-1/2 flex h-3/4 md:h-full ">
            <TreeView
              boms={boms}
              selectedItem={selectedItem}
              setSelectedItem={setSelectedItem}
            />
          </div>
          <div className="w-full md:w-1/2 flex h-1/4 md:h-full ">
            <TreeDataView
              selectedItem={selectedItem}
              populating={populated}
              populateData={populateData}
            />
          </div>
        </div>
      </div>
      <div className="w-full flex ">
        <div className="w-full h-1/2 ">
          <DataGridView boms={boms} parts={parts} selectedItem={selectedItem} />
        </div>
      </div>
    </div>
  )
}

export default App
