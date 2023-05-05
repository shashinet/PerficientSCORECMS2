import React from 'react'
import SiteMapSelector from '../components/sitemapSelector';

export default {
  title: 'Sitemap Selector'
}

const sites = [
  { guid: 1, siteUrl: '/sitemap-1', siteName: 'site-1' },
  { guid: 2, siteUrl: '/sitemap-2', siteName: 'site-2' },
  { guid: 3, siteUrl: '/sitemap-3', siteName: 'site-3' },
  { guid: 4, siteUrl: '/sitemap-4', siteName: 'site-4' },
];

const Template = (args) => <SiteMapSelector {...args} />

export const SitemapSelector = Template.bind({});

SitemapSelector.args = {
  sites: sites,
  currentSite: 'Site 1',
}
