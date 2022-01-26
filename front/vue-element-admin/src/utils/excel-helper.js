export function toExcelfile(response) {
  console.log(response.headers['content-disposition'])
  var filename =
          response.headers['content-disposition'] &&
          response.headers['content-disposition']
            .split(';')[1]
            .split('filename=')[1]
  if (filename) {
    filename =
          response.headers['content-disposition'] &&
          response.headers['content-disposition'].split(';')[2]
            .split("filename*=UTF-8''")[1]
  }
  if (filename) {
    if (filename.indexOf('%') < 0) {
      filename = decodeURIComponent(escape(filename))
    } else {
      filename = decodeURIComponent(filename)
    }
  }
  const stream = response.data // 后端用stream返回Excel文件
  const blob = new Blob([stream], {
    type: 'applicaiton/vnd.ms-excel'
  })
  const downloadElement = document.createElement('a')
  const href = window.URL.createObjectURL(blob) // 创建下载的链接
  downloadElement.href = href
  downloadElement.download = filename // 下载后文件名
  document.body.appendChild(downloadElement)
  downloadElement.click() // 点击下载
  document.body.removeChild(downloadElement) // 下载完成移除元素
  window.URL.revokeObjectURL(href) // 释放掉blob对象
}
