import { Bom } from "../App"
interface TreeDataViewProps {
  selectedItem: Bom | null
  populating: boolean
  populateData: () => void
  error?: string
}
const TreeDataView = (props: TreeDataViewProps) => {
  const { selectedItem, populating, populateData, error } = props
  return (
    <div className="md:px-12 w-full">
      <div className="flex md:flex-col md:gap-6 flex-row justify-between">
        <div className="flex flex-col w-1/2 md:w-full gap-2">
          <div>Parent Child Part: {selectedItem?.parentName}</div>
          <div>Current Part: {selectedItem?.componentName}</div>
        </div>
        <button
          className="bg-blue-500 hover:bg-blue-700 w-fit h-fit text-white font-bold py-2 px-4 rounded disabled:opacity-50 disabled:bg-gray-400"
          disabled={populating}
          type="button"
          onClick={populateData}
        >
          Populate Data in Tree
        </button>
      </div>
      {error && <div className="text-red-500">{error}</div>}
    </div>
  )
}
export default TreeDataView
