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

  const combine = useMemo(() => {
    const result = boms.map((bom) => {
      const part = parts.find((part) => part.name === bom.componentName)
      return {
        ...bom,
        ...part,
      }
    })
    return result
  }, [boms, parts])

  const renderData = () => {
    const filteredResult = combine.filter(
      (item) => item.parentName === selectedItem?.componentName
    )
    return filteredResult
  }

  const data = renderData()

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
    <div className="relative px-6 md:px-12 overflow-auto overflow-y-scroll h-full overflow-x-auto">
      <table className="w-full lg:table-fixed border-2 border-black h-full">
        <thead className="">
          <tr className="border-b border-blue-gray-100 bg-blue-gray-50 ">
            {TABLE_HEADINGS.map((heading, index) => (
              <th key={index} className="py-2 text-start text-xs sm:text-sm">
                {heading}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className="bg-gray-50 overflow-y-scroll overflow-auto h-full bg-blue-gray-100">
          {data.map((item, index) => {
            const isLast = index === data.length - 1
            const className = isLast ? "" : "border-b border-blue-gray-100"
            return (
              <tr key={index} className="">
                <td className={className}>{item.parentName}</td>
                <td className={className}>{item.componentName}</td>
                <td className={className}>{item.partNumber}</td>
                <td className={className}>{item.title}</td>
                <td className={className}>{item.quantity}</td>
                <td className={className}>{item.type}</td>
                <td className={className}>{item.item}</td>
                <td className={className}>{item.material}</td>
              </tr>
            )
          })}
        </tbody>
      </table>
    </div>
  )
}

export default DataGridView
