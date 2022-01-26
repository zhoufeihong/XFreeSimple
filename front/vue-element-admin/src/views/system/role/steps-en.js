const steps = [
  {
    element: '#btn-add',
    popover: {
      title: 'Adding roles',
      description: 'Add role information and grant corresponding permissions to roles. After a user is assigned a role, the user is assigned the rights of the role.',
      position: 'bottom'
    }
  },
  {
    element: '#btn-permission',
    popover: {
      title: 'Assign rights to roles',
      description: 'Permission tree Node Operation Description <br> Click the arrow to expand/shrink the tree. <br> Click the check box to select/deselect the current node. <br> Click the node content to select/deselect the current node and all its child nodes',
      position: 'left'
    }
  }
]

export default steps
