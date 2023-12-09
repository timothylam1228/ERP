import { useMemo } from "react"
import { Bom } from "../App"
import { Part } from "../App"

interface DataGridViewProps {
  boms: Bom[]
  parts: Part[]
  selectedItem: Bom | null
}
const DataGridView = (props: DataGridViewProps) => {
  const { boms, parts, selectedItem } = props

  const data = useMemo(() => {
    return boms
      .map((bom) => ({
        ...bom,
        ...parts.find((part) => part.name === bom.componentName),
      }))
      .filter((item) => item.parentName === selectedItem?.componentName)
  }, [boms, parts, selectedItem])

  const TABLE_HEADINGS = [
    "PARENT_NAME",
    "COMPONENT_NAME",
    "PART_NUMBER",
    "TITLE",
    "QUANTITY",
    "TYPE",
    "ITEM",
    "MATERIAL",
  ]
  return (
    <div className="relative flex flex-col  mx-6 md:mx-12 border-2 border-black">
      {/* Table for thead */}
      <table className="w-full lg:table-fixed"></table>

      {/* Scrollable table for tbody */}
      <div className="overflow-y-auto h-64 lg:h-96 overflow-x-auto">
        {/* Adjust 'h-64' to your preferred height */}
        <table className="w-full lg:table-fixed  table-auto overflow-auto">
          <thead className="bg-blue-gray-50">
            <tr className="border-b border-blue-gray-100">
              {TABLE_HEADINGS.map((heading, index) => (
                <th key={index} className="py-2 text-start text-xs sm:text-sm">
                  {heading}
                </th>
              ))}
            </tr>
          </thead>
          <tbody className="bg-gray-50">
            {data.map((item, index) => (
              <tr key={index} className="border-b border-blue-gray-100">
                <td>{item.parentName}</td>
                <td>{item.componentName}</td>
                <td>{item.partNumber}</td>
                <td>{item.title}</td>
                <td>{item.quantity}</td>
                <td>{item.type}</td>
                <td>{item.item}</td>
                <td>{item.material}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default DataGridView
