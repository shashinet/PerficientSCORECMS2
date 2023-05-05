import React, { useState } from 'react';
import { Button, Input, Fieldset, Table } from 'optimizely-oui';

function SiteMapQuantitySelector(props) {
    const {quantities} = props;
    return (
        <div className="sitemap-selector">
            {quantities.map((quantity, index) => (
                <div key={index}>
                    <Fieldset>
                        <div className='column-right grid-align-right'>
                            <Button
                                data type="submit"
                                style={quantity.editMasterSiteMapBtnStyle}
                                title={quantity.editMasterSiteMapBtnTitle}
                                // onClick={() => { window.location.href = selectedSiteUrl; }} // This needs to be set to Edit Master Sitemap.
                            >
                                {quantity.editMasterSiteMapBtnText}
                            </Button>
                        </div>
                    </Fieldset>
                    <Fieldset>
                        <div className='column-left flex-start'>
                            <label for="sitemap-name-input">{quantity.siteMapNameLabel} <span class="oui-label--required"></span></label>
                            <Input id="sitemap-name-input" className='sitemap-name-input' placeholder={quantity.siteMapNameDefaultText} isRequired={true} type="text" />
                        </div>
                        <div className='column-right grid-align-right'>
                            <Button
                                rightIcon={quantity.addSiteMapNodeBtnIcon}
                                // onClick={() => { window.location.href = selectedSiteUrl; }} // This will be the plus button for adding nodes.
                                style={quantity.addSiteMapNodeBtnStyle}
                                title={quantity.addSiteMapNodeBtnTitle}
                                className='add-edit-button'
                                size='large'>
                                    {quantity.addSiteMapNodeBtnText}
                                </Button>
                        </div>
                    </Fieldset>
                    <Table density="loose" style="rule" tableLayoutAlgorithm="auto" className='sitemap-node-table'>
                        <Table.THead className='sitemap-node-table--header'>
                            <Table.TR className='sitemap-node-table--header--row'>
                                <Table.TH className='sitemap-node-table--header--cell' width="20%">Sitemap Name</Table.TH>
                                <Table.TH className='sitemap-node-table--header--cell'>Type</Table.TH>
                                <Table.TH className='sitemap-node-table--header--cell'>Description</Table.TH>
                                <Table.TH className='sitemap-node-table--header--cell'>Language</Table.TH>
                                <Table.TH isCollapsed={true} className='sitemap-node-table--header--cell'>Edit</Table.TH>
                            </Table.TR>
                        </Table.THead>
                        <Table.TBody className='sitemap-node-table--body'>
                            {quantity.sitemaps.map((sitemap, index) => {
                                return (                                    
                                    <Table.TR noBorder={true} className='sitemap-node-table--body--row' key={index}>
                                        <Table.TD className='sitemap-node-table--body--cell'>{sitemap.siteMapName}</Table.TD>
                                        <Table.TD className='sitemap-node-table--body--cell'>{sitemap.siteMapType}</Table.TD>
                                        <Table.TD className='sitemap-node-table--body--cell'>{sitemap.siteMapDescription}</Table.TD>
                                        <Table.TD className='sitemap-node-table--body--cell'>{sitemap.siteMapLangauge}</Table.TD>
                                        <Table.TD className='sitemap-node-table--body--cell edit-cell'>
                                            <Button
                                                rightIcon={quantity.editSiteMapNodeBtnIcon}
                                                // onClick={() => { window.location.href = selectedSiteUrl; }} // This will be the edit button for editing current sitemap node.
                                                size="small"
                                                style={quantity.editSiteMapNodeBtnStyle}
                                                title={quantity.editSiteMapNodeBtnTitle}
                                                width="default"
                                                className="add-edit-button">
                                                    {quantity.editSiteMapNodeBtnText}
                                            </Button>
                                        </Table.TD>
                                    </Table.TR>
                                );
                            })}
                        </Table.TBody>
                    </Table>
                </div>
            ))}
        </div>
    );
}

export default SiteMapQuantitySelector;