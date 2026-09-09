export var UserStatus;
(function (UserStatus) {
    UserStatus[UserStatus["Pending"] = 1] = "Pending";
    UserStatus[UserStatus["Active"] = 2] = "Active";
    UserStatus[UserStatus["Inactive"] = 3] = "Inactive";
    UserStatus[UserStatus["Suspended"] = 4] = "Suspended";
    UserStatus[UserStatus["Locked"] = 5] = "Locked";
    UserStatus[UserStatus["Block"] = 6] = "Block";
    UserStatus[UserStatus["Unblock"] = 7] = "Unblock";
})(UserStatus || (UserStatus = {}));
