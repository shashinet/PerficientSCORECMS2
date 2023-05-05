/* eslint-disable react/style-prop-object */
/* eslint-disable arrow-body-style */
import React, { useState } from 'react';
import { Dropdown, Button } from 'optimizely-oui';
import PropTypes from 'prop-types';

function SiteMapSelector(props) {
  const { sites, currentSite } = props;
  const [selectedSiteName, setCurrentSiteName] = useState(currentSite);
  const [selectedSiteUrl, setSelectedSiteUrl] = useState('/');

  const selectSite = (site) => {
    setCurrentSiteName(site.siteName);
    setSelectedSiteUrl(site.siteUrl);
  };

  return (
    <div className="sitemap-selector">
      <h2>Site Map Selector</h2>
      <Dropdown buttonContent={selectedSiteName} arrowIcon="down" width={300}>
        <Dropdown.Contents>
          {sites.map((site, index) => {
            return (
              <Dropdown.ListItem key={index}>
                <Dropdown.BlockLink onClick={() => { selectSite(site); }}>
                  <Dropdown.BlockLinkText text={site.siteName} />
                </Dropdown.BlockLink>
              </Dropdown.ListItem>
            );
          })}
        </Dropdown.Contents>
      </Dropdown>
      {/* <Button data type="submit" style="highlight" title="My Button" onClick={() => { window.location.href = selectedSiteUrl; }}>Go</Button> */}
    </div>
  );
}

SiteMapSelector.propTypes = {
  sites: PropTypes.arrayOf(PropTypes.shape({
    guid: PropTypes.number.isRequired,
    siteUrl: PropTypes.string.isRequired,
    siteName: PropTypes.string.isRequired,
  })).isRequired,
  currentSite: PropTypes.string.isRequired,
};

export default SiteMapSelector;
