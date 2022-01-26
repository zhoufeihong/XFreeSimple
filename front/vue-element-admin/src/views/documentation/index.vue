<template>
  <div class="app-container documentation-container">
    <dropdown-menu class="document-btn" :items="articleList" :title="$t('documentation.documentation')" />
  </div>
</template>

<script>
import DropdownMenu from '@/components/Share/DropdownMenu'

export default {
  name: 'Documentation',
  components: { DropdownMenu },
  data() {
    return {
      articleList: [
        { title: 'Github XFreeSimple', href: 'https://github.com/zhoufeihong/XFreeSimple' },
        { title: 'Gitee XFreeSimple', href: 'https://gitee.com/RiverBied/XFreeSimple' },
        { title: 'ABP', href: 'https://docs.abp.io/en/abp/latest' },
        { title: 'EF Core', href: 'https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=fluent-api' },
        { title: 'vue-element-admin', href: 'https://panjiachen.github.io/vue-element-admin-site/zh/' },
        { title: 'Element UI', href: 'https://element.eleme.cn/#/zh-CN/component/installation' },
        { title: 'JeecgBoot', href: 'http://doc.jeecg.com/2043868' }
      ]
    }
  }
}
</script>

<style lang="scss" scoped>
.documentation-container {
  margin: 50px;
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-start;

  .document-btn {
    flex-shrink: 0;
    display: block;
    cursor: pointer;
    background: rgb(158, 156, 156);
    color: white;
    height: 60px;
    padding: 0 16px;
    margin: 16px;
    line-height: 60px;
    font-size: 20px;
    text-align: center;
  }
}
</style>
