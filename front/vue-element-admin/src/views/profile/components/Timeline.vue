<template>
  <div class="block">
    <el-timeline>
      <el-timeline-item v-for="(item,index) of timeline" :key="index" :timestamp="item.timestamp" placement="top">
        <el-card>
          <h4>{{ item.title }}</h4>
        </el-card>
      </el-timeline-item>
    </el-timeline>
  </div>
</template>

<script>

import { getOperationInfoListPaged } from '@/api/system/user'

export default {
  data() {
    return {
      timeline: []
    }
  },
  created() {
    this.getListPagedResult()
  },
  methods: {
    async getListPagedResult() {
      const { data } = await getOperationInfoListPaged(1, 20)
      var items = data.data
      if (Array.isArray(items)) {
        if (items.length === 0) return
        this.timeline = items.map((v, i) => {
          return {
            timestamp: v.creationTime,
            title: v.title,
            content: ''
          }
        })
      }
    }
  }
}
</script>
