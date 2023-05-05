import React from 'react';
import PropTypes from 'prop-types';

/**
 *
 * @param props
 * @returns {JSX.Element}
 * @constructor
 */

export default function MenuList(props) {
  const { block } = props;
  return (
    <>
      <ul className="menu-list-section" role="menu">
        <li className="menu-list-section-item" role="presentation">
          {block.sectionTitle && (
            <a
              href={block.sectionUrl}
              role="menuitem"
              aria-label={block.sectionTitle}
              className="menu-list-section-link"
            >
              {block.sectionTitle}
            </a>
          )}
          <ul className="menu-list" role="menu">
            {React.Children.toArray(
              block.menuListContent.map((item) => (
                <li className="menu-list-item" role="presentation">
                  <a
                    href={item.url}
                    role="menuitem"
                    target={item.openInNewWindow ? '_blank' : '_self'}
                    rel="noreferrer"
                  >
                    {item.title}
                  </a>
                </li>
              )),
            )}
          </ul>
        </li>
      </ul>
    </>
  );
}

MenuList.propTypes = {
  block: PropTypes.shape({
    sectionTitle: PropTypes.string,
    sectionUrl: PropTypes.string,
    menuListContent: PropTypes.arrayOf(PropTypes.shape({
      url: PropTypes.string,
      title: PropTypes.string,
      openInNewWindow: PropTypes.bool,
    })),
  }),
};

MenuList.defaultProps = {
  block: {},
};
