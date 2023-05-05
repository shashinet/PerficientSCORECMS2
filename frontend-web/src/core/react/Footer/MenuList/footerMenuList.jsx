import React from 'react';

import { footerMenuDefaultTypes, footerMenuTypes } from '../types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function FooterMenuList(props) {
  const { title, link, linkAriaLebel, openInNewWindow, childPages } = props;
  return (
    <>
      <div className="footer-menu-list">
        <ul className="menu-list" role="menu">
          <li className="menu-list-parent" role="menuitem">
            <a href={link} target={`${openInNewWindow ? '_blank' : '_self'}`} aria-label={linkAriaLebel}>
              {title}
            </a>
            {childPages.length ? (
              <ul className="menu-list">
                {childPages.map((child) => (
                  <li className="sub-menu-list" key={child.title}>
                    <a
                      href={child.link}
                      target={`${child.openInNewWindow ? '_blank' : '_self'}`}
                      aria-label={child.linkAriaLebel}
                    >
                      {child.title}
                    </a>
                  </li>
                ))}
              </ul>
            ) : null}
          </li>
        </ul>
      </div>
    </>
  );
}

FooterMenuList.propTypes = {
  ...footerMenuTypes,
};
FooterMenuList.defaultProps = {
  ...footerMenuDefaultTypes,
};
