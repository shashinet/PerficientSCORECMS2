import React from "react";
import SiteMapQuantitySelector from "../components/sitemapQuantitySelector";

export default {
  title: 'Sitemap Quantity Selector'
}

const quantities = [
    {
        addSiteMapNodeBtnIcon: 'add', // Button icon
        addSiteMapNodeBtnText: 'Add', // Button text
        addSiteMapNodeBtnTitle: 'Add Site Map Node', // Tooltip text
        addSiteMapNodeBtnStyle: 'highlight', // Button style, i.e. danger, danger-outline, highlight, outline, plain

        editMasterSiteMapBtnStyle: 'highlight', // Button style, i.e. danger, danger-outline, highlight, outline, plain
        editMasterSiteMapBtnText: 'Manually Edit Master Sitemap', // Button text
        editMasterSiteMapBtnTitle: 'Manually Edit Master Sitemap', // Tooltip text

        editSiteMapNodeBtnIcon: 'pencil', // Button icon
        editSiteMapNodeBtnStyle: 'plain', // Button style, i.e. danger, danger-outline, highlight, outline, plain
        editSiteMapNodeBtnText: 'Edit', // Button text
        editSiteMapNodeBtnTitle: 'Edit Site Map Node', // Tooltip text

        selectedSiteName: 'site-1', // Comes from site selector - sites
        siteMapNameDefaultText: 'sitemap.xml', // Placeholder text
        siteMapNameLabel: 'Sitemap Name', // Sitemap Name label text for input

        // Populates sitemap table
        sitemaps: [
            {
                siteMapId: 1,
                siteMapName: 'Internal Example',
                siteMapType: 'Internal',
                siteMapDescription: 'Internal description',
                siteMapLangauge: 'EN'
            },
            {
                siteMapId: 2,
                siteMapName: 'Manual Example',
                siteMapType: 'Manual',
                siteMapDescription: 'Manual description',
                siteMapLangauge: 'ES'
            },
            {
                siteMapId: 3,
                siteMapName: 'External Example',
                siteMapType: 'External',
                siteMapDescription: 'External description',
                siteMapLangauge: 'EN'
            }
        ]
    }
];

const Template = (args) => <SiteMapQuantitySelector {...args} />

export const SitemapQuantitySelector = Template.bind({});

SitemapQuantitySelector.args = {
    quantities: quantities
}