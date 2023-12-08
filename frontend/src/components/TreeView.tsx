import { useMemo, useState } from "react"
import { Bom } from "../App"

interface TreeNodeProps {
  node: Bom
  selectedItem: Bom | null
  handleSelect: (arg: Bom) => void
}

interface TreeViewProps {
  boms: Bom[]
  setSelectedItem: (arg: Bom | null) => void
  selectedItem: Bom | null
}

const TreeNode: React.FC<TreeNodeProps> = ({
  node,
  selectedItem,
  handleSelect,
}) => {
  const [isOpen, setIsOpen] = useState(false)
  const onSelect = (node: Bom) => {
    handleSelect(node)
  }

  const toggleOpen = (event: React.MouseEvent<HTMLButtonElement>) => {
    console.log("toggleOpen")
    setIsOpen(!isOpen)
    event.stopPropagation() // Prevent triggering select when toggling
  }
  return (
    <li className={`cursor-pointer`}>
      <div className={`flex items-center `}>
        <div className="w-12 ">
          {node.children && node.children.length > 0 && (
            <button onClick={(event) => toggleOpen(event)} className="w-full">
              {isOpen ? "-" : "+"}
            </button>
          )}
        </div>
        {node.componentName && (
          <div
            className={`hover:bg-red-50 ${
              selectedItem?.componentName === node.componentName
                ? "bg-red-200"
                : ""
            }  w-full py-1 px-2 `}
            onClick={() => onSelect(node)}
          >
            {node.componentName}
          </div>
        )}
      </div>
      {isOpen && node.children && (
        <ul className={`ml-4 `}>
          {node.children.map((childNode, index) => (
            <TreeNode
              key={index}
              node={childNode}
              handleSelect={handleSelect}
              selectedItem={selectedItem}
            />
          ))}
        </ul>
      )}
    </li>
  )
}

const TreeView = (props: TreeViewProps) => {
  const { boms, setSelectedItem, selectedItem } = props
  const convertBomsToTree = (items: Bom[]): Bom[] => {
    const map = new Map<string, Bom>(
      items.map((item) => [item.componentName, { ...item, children: [] }])
    )
    const tree: Bom[] = []
    items.forEach((item: Bom) => {
      if (item.parentName) {
        const parent = map.get(item.parentName)
        if (parent) {
          parent.children = parent.children || []
          parent.children.push(map.get(item.componentName)!)
        }
      } else {
        tree.push(map.get(item.componentName)!)
      }
    })
    return tree
  }

  const bomTree = useMemo(() => convertBomsToTree(boms), [boms])

  const handleSelect = (item: Bom) => {
    if (item === selectedItem) {
      setSelectedItem(null)
    } else {
      setSelectedItem(item)
    }
  }

  return (
    <div className="border-2 border-black flex h-full w-full overflow-y-scroll items-start shadow-lg">
      <div className="flex flex-col w-full">
        <ul className="list-disc ">
          {bomTree.map((node, index) => (
            <TreeNode
              key={index}
              node={node}
              selectedItem={selectedItem}
              handleSelect={handleSelect}
            />
          ))}
        </ul>
      </div>
    </div>
  )
}

export default TreeView
